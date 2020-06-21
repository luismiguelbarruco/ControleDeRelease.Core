using ControleDeRelease.Domain.Helpers;
using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var result = RunAsync();

            return result.Result;
        }

        private async Task<List<ItemLiberacaoRelease>> RunAsync()
        {
            var thisLock = new object();
            var itens = new List<ItemLiberacaoRelease>();

            await Task.Run(() =>
            {
                Parallel.ForEach(Projetos, projeto =>
                {
                    var itemLiberacaoRelease = AnalisarItemLiberacaoRelease(projeto);

                    itens.Add(itemLiberacaoRelease);

                    lock (thisLock)
                    {
                        AddNotifications(itemLiberacaoRelease.Notifications);
                    }
                });
            });

            return itens;
        }

        private ItemLiberacaoRelease AnalisarItemLiberacaoRelease(Projeto projeto)
        {
            var itemLiberacaoRelease = new ItemLiberacaoRelease(projeto);

            var pathRelease = $@"{Versao.DiretorioRelease}\{projeto.Path}";
            var pathTeste = $@"{Versao.DiretorioTeste}\{projeto.Path}";

            //var pathRelease = $@"D:\Downloads\Sql Server\SQLServer2019-DEV-x64-ENU.exe";
            //var pathTeste = $@"D:\Downloads\Sql Server\SQLServer2019-DEV-x64-ENU.exe";

            if (itemLiberacaoRelease.Validate(pathRelease))
                itemLiberacaoRelease.ReleaseAttriburesDiretorioRelese = FileInfoHelper.GetDataFileVersion(pathRelease);

            if (itemLiberacaoRelease.Validate(pathTeste))
                itemLiberacaoRelease.ReleaseAttriburesDiretorioTeste = FileInfoHelper.GetDataFileVersion(pathTeste);

            if (!itemLiberacaoRelease.Notifications.Any())
                itemLiberacaoRelease.SetStatusRelease();

            return itemLiberacaoRelease;
        }
    }
}