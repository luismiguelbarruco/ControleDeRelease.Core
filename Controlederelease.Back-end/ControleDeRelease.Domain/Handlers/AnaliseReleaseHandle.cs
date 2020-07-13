using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.AnaliseRelease;
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Queries;
using ControleDeRelease.Domain.Repository;
using ControleDeRelease.Domain.VireModel;
using Flunt.Notifications;
using System;
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

            var projetos = _projetoRepository.Selecionar(versao).ToList();

            if(!projetos.Any())
                return new CommandResult(false, "Nenhum projeto encontrado");

            var releaseAnalizerService = new ReleaseAnalizerService(versao, projetos);

            var analizerServiceResult = releaseAnalizerService.Run();

            if (releaseAnalizerService.Notifications.Any())
                return new CommandResult(false, "Não foi possivel analisar as releases", releaseAnalizerService.Notifications);

            var itens = new ItensLiberacaoReleaseViewModel().Parse(analizerServiceResult);

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
                return new CommandResult(false, $"Versão não encontrada");

            var liberacaoRelease = new LiberacaoRelease
            {
                Versao = command.Versao,
                Data = DateTime.Now,
                Itens = new ItensLiberacaoRelease().Parse(command.Itens)
            };

            if (!_liberacaoReleaseRepository.Cadastar(liberacaoRelease))
                return new CommandResult(false, "Ocorreu erro ao cadastrar os dados da liberação de release.");

            return new CommandResult(true, "Liberação de release cadastrado com sucesso.");
        }
    }
}
