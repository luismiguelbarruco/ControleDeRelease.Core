using ControleDeRelease.Domain.Helpers;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeRelease.Domain.Entities
{
    public class LiberacaoRelease : Notifiable
    {
        public List<ItemLiberacaoRelease> Items { get; set; }

        public LiberacaoRelease() => Items = new List<ItemLiberacaoRelease>();

        public void AnalisarReleases(Versao versao, List<Projeto> projetos)
        {
            foreach (var projeto in projetos)
            {
                var itemLiberacaoRelease = new ItemLiberacaoRelease(projeto);

                var pathRelease = $@"{versao.DiretorioRelease}\{projeto.Path}";
                var pathTeste = $@"{versao.DiretorioTeste}\{projeto.Path}";

                if (itemLiberacaoRelease.Validate(pathRelease))
                    itemLiberacaoRelease.DadosVersaoRelease = FileInfoHelper.GetDataFileVersion(pathRelease);

                if (itemLiberacaoRelease.Validate(pathTeste))
                    itemLiberacaoRelease.DadosVersaoTeste = FileInfoHelper.GetDataFileVersion(pathTeste);

                if (!itemLiberacaoRelease.Notifications.Any())
                    itemLiberacaoRelease.SetStatusRelease();

                Items.Add(itemLiberacaoRelease);

                AddNotifications(itemLiberacaoRelease.Notifications);
            }
        }
    }
}
