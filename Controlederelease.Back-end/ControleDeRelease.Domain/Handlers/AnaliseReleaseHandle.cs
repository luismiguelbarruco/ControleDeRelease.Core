using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.AnaliseRelease;
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Queries;
using ControleDeRelease.Domain.Repository;
using ControleDeRelease.Domain.VireModel;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeRelease.Domain.Handlers
{
    public class AnaliseReleaseHandle : Notifiable, IHandle<AnalisarReleasesCommand>
    {
        private readonly IVersaoProjetoRepository _versaoProjetoRepository;
        private readonly IProjetoRepository _projetoRepository;

        public AnaliseReleaseHandle(IVersaoProjetoRepository versaoProjetoRepository, IProjetoRepository projetoRepository)
        {
            _versaoProjetoRepository = versaoProjetoRepository;
            _projetoRepository = projetoRepository;
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

            var liberacaoRelease = new LiberacaoRelease();
            liberacaoRelease.AnalisarReleases(versao, projetos);

            if (liberacaoRelease.Notifications.Any())
                return new CommandResult(false, "Não foi possivel analisar as releases", liberacaoRelease.Notifications);

            var itens = GetItensLiberacaoReleaseVireModel(liberacaoRelease.Items);

            return new CommandResult(true, itens);
        }

        public List<ItemLiberacaoReleaseVireModel> GetItensLiberacaoReleaseVireModel(List<ItemLiberacaoRelease> itensLiberacaoReleases)
        {
            var itensLiberacaoReleaseVireModel = new List<ItemLiberacaoReleaseVireModel>();

            foreach (var item in itensLiberacaoReleases)
            {
                var itemLiberacaoReleaseVireModel = new ItemLiberacaoReleaseVireModel().Parse(item);
                itensLiberacaoReleaseVireModel.Add(itemLiberacaoReleaseVireModel);    
            }

            return itensLiberacaoReleaseVireModel;
        }
    }
}
