using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.AnaliseRelease;
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Poco;
using ControleDeRelease.Domain.Queries;
using ControleDeRelease.Domain.Repository;
using ControleDeRelease.Domain.VireModel;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeRelease.Domain.Handlers
{
    public class AnaliseReleaseHandle : Notifiable, IHandle<AnalisarReleasesCommand>, IHandle<InserirLiberacaoReleaseCommand>
    {
        private readonly IVersaoProjetoRepository _versaoProjetoRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly ILiberacaoReleaseRepository _liberacaoReleaseRepository;

        public AnaliseReleaseHandle(
            IVersaoProjetoRepository versaoProjetoRepository, 
            IProjetoRepository projetoRepository,
            ILiberacaoReleaseRepository liberacaoReleaseRepository = null
        )
        {
            _versaoProjetoRepository = versaoProjetoRepository;
            _projetoRepository = projetoRepository;
            _liberacaoReleaseRepository = liberacaoReleaseRepository;
        }

        public ICommandResult Handler(AnalisarReleasesCommand command)
        {
            command.Validate();

            if(command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel analisar as releases", command.Notifications);
            }

            var query = VersaoQueries.Selecionar(command.Id);

            var versao = _versaoProjetoRepository.Selecionar(query);

            if (versao == null)
                return new CommandResult(false, $"Versão não encontrada");

            var projetos = _projetoRepository.Selecionar().ToList();

            if(!projetos.Any())
                return new CommandResult(false, "Nenhum projeto encontrado");

            var releaseAnalizer = new ReleaseAnalizer(versao, projetos);
            var analizerResult = releaseAnalizer.Run();

            if (releaseAnalizer.Notifications.Any())
                return new CommandResult(false, "Não foi possivel analisar as releases", releaseAnalizer.Notifications);

            var itens = GetItensLiberacaoReleaseVireModel(analizerResult);

            return new CommandResult(true, itens);
        }

        public ICommandResult Handler(InserirLiberacaoReleaseCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel inserir os dados", command.Notifications);
            }

            var query = VersaoQueries.Selecionar(command.Versao.Id);

            var versao = _versaoProjetoRepository.Selecionar(query);

            if(versao == null)
            {
                return new CommandResult(false, $"Versão não encontrada");
            }

            var liberacaoRelease = new LiberacaoRelease
            {
                Versao = command.Versao,
                Data = DateTime.Now,
                Itens = GetItensLiberacaoRelease(command.Itens)
            };

            if(!_liberacaoReleaseRepository.Cadastar(liberacaoRelease))
                return new CommandResult(false, "Ocorreu erro ao cadastrar os dados da liberação de release.");

            return new CommandResult(true, "Liberação de release cadastrado com sucesso.");
        }

        public List<ItemLiberacaoReleaseVireModel> GetItensLiberacaoReleaseVireModel(List<ItemLiberacaoRelease> itensLiberacaoRelease)
        {
            var itensLiberacaoReleaseVireModel = new List<ItemLiberacaoReleaseVireModel>();

            foreach (var item in itensLiberacaoRelease)
            {
                var itemLiberacaoReleaseVireModel = new ItemLiberacaoReleaseVireModel().Parse(item);
                itensLiberacaoReleaseVireModel.Add(itemLiberacaoReleaseVireModel);
            }

            return itensLiberacaoReleaseVireModel;
        }

        public List<ItemLiberacaoReleasePoco> GetItensLiberacaoRelease(List<ItemLiberacaoReleaseVireModel> itensLiberacaoReleaseViewModel)
        {
            var itensLiberacaoRelease = new List<ItemLiberacaoReleasePoco>();

            foreach (var item in itensLiberacaoReleaseViewModel)
            {
                var itemLiberacaoReleaseVireModel = new ItemLiberacaoReleasePoco().Parse(item);
                itensLiberacaoRelease.Add(itemLiberacaoReleaseVireModel);
            }

            return itensLiberacaoRelease;
        }
    }
}
