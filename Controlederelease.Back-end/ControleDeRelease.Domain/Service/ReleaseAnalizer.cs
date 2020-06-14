using ControleDeRelease.Domain.Helpers;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeRelease.Domain.Entities
{
    public class ReleaseAnalizer : Notifiable
    {
        public Versao Versao { get; private set; }

        public List<Projeto> Projetos { get; private set; }

        public ReleaseAnalizer(Versao versao, List<Projeto> projetos)
        {
            Versao = versao;
            Projetos = projetos;
        }

        public List<ItemLiberacaoRelease> Run()
        {
            var itens = new List<ItemLiberacaoRelease>();

            //addionar tramanento de excessão
            foreach (var projeto in Projetos)
            {
                var itemLiberacaoRelease = new ItemLiberacaoRelease(projeto);

                var pathRelease = $@"{Versao.DiretorioRelease}\{projeto.Path}";
                var pathTeste = $@"{Versao.DiretorioTeste}\{projeto.Path}";

                if (itemLiberacaoRelease.Validate(@"C:\Program Files (x86)\WinRAR\WinRAR.exe"))
                    itemLiberacaoRelease.ReleaseAttriburesDiretorioRelese = FileInfoHelper.GetDataFileVersion(@"C:\Program Files (x86)\WinRAR\WinRAR.exe");

                if (itemLiberacaoRelease.Validate(@"C:\Program Files (x86)\WinRAR\WinRAR.exe"))
                    itemLiberacaoRelease.ReleaseAttriburesDiretorioTeste = FileInfoHelper.GetDataFileVersion(@"C:\Program Files (x86)\WinRAR\WinRAR.exe");

                if (!itemLiberacaoRelease.Notifications.Any())
                    itemLiberacaoRelease.SetStatusRelease();

                itens.Add(itemLiberacaoRelease);

                AddNotifications(itemLiberacaoRelease.Notifications);
            }

            return itens;
        }
    }
}