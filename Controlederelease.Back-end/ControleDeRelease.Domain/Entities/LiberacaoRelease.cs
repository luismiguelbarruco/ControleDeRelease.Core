using ControleDeRelease.Domain.Helpers;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeRelease.Domain.Entities
{
    public class LiberacaoRelease : Notifiable
    {
        public List<ItemLiberacaoRelease> Items { get; set; }

        public LiberacaoRelease()
        {
            Items = new List<ItemLiberacaoRelease>();
        }

        public void AnalisarReleases(Versao versao, List<Projeto> projetos)
        {
            projetos.ForEach(projeto =>
            {
                Version releaseVersion = null;
                Version testeVersion = null;

                var pathRelease = $@"{versao.DiretorioRelease}\{projeto.Path}";
                var pathTeste = $@"{versao.DiretorioTeste}\{projeto.Path}";

                var itemLiberacaoRelease = new ItemLiberacaoRelease(projeto.Nome);

                if (itemLiberacaoRelease.Validate(pathRelease))
                {
                    itemLiberacaoRelease.AttributeFileRelease = FileInfoHelper.GetAttributeFile(pathRelease);
                    releaseVersion = itemLiberacaoRelease.AttributeFileRelease.Versao;
                }

                if (itemLiberacaoRelease.Validate(pathTeste))
                {
                    itemLiberacaoRelease.AttributeFileTeste = FileInfoHelper.GetAttributeFile(pathTeste);
                    testeVersion = itemLiberacaoRelease.AttributeFileTeste.Versao;
                }

                if (!itemLiberacaoRelease.Notifications.Any())
                    itemLiberacaoRelease.SetStatusRelease(FileInfoHelper.CompareVersion(releaseVersion, testeVersion));

                Items.Add(itemLiberacaoRelease);

                AddNotifications(itemLiberacaoRelease.Notifications);
            });
        }
    }
}
