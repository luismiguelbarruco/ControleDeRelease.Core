using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.AnaliseRelease;
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Helpers;
using ControleDeRelease.Domain.Queries;
using ControleDeRelease.Domain.Repository;
using Flunt.Notifications;
using System;
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

            var liberacaoRelease = AnalisarReleases(versao, projetos);

            if(liberacaoRelease.Notifications.Any())
                return new CommandResult(false, "Não foi possivel analisar as releases", liberacaoRelease.Notifications);

            return new CommandResult(true, liberacaoRelease);
        }

        private LiberacaoRelease AnalisarReleases(Versao versao, List<Projeto> projetos)
        {
            var liberacaoRelease = new LiberacaoRelease();

            projetos.ForEach(projeto =>
            {
                Version releaseVersion = null;
                Version testeVersion = null;

                var pathRelease = $@"{versao.DiretorioRelease}\{projeto.Path}";
                var pathTeste = $@"{versao.DiretorioTeste}\{projeto.Path}";

                var itemLiberacaoRelease = new ItemLiberacaoRelease(projeto.Nome);

                if(itemLiberacaoRelease.Validate(pathRelease))
                {
                    itemLiberacaoRelease.AttributeFileRelease = (AttributeFileRelease)FileInfoHelper.GetAttributeFile(pathRelease);
                    releaseVersion = itemLiberacaoRelease.AttributeFileRelease.Versao;
                }

                if (itemLiberacaoRelease.Validate(pathTeste))
                {
                    itemLiberacaoRelease.AttributeFileTeste = (AttributeFileTeste)FileInfoHelper.GetAttributeFile(pathTeste);
                    testeVersion = itemLiberacaoRelease.AttributeFileTeste.Versao;
                }

                if(!itemLiberacaoRelease.Notifications.Any())
                    itemLiberacaoRelease.StatusAtualizacao = FileInfoHelper.CompareVersion(releaseVersion, testeVersion);

                liberacaoRelease.Items.Add(itemLiberacaoRelease);

                liberacaoRelease.AddNotifications(itemLiberacaoRelease.Notifications);
            });

            return liberacaoRelease;
        }
    }
}
