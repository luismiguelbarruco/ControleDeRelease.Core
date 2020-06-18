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

        public async Task<List<ItemLiberacaoRelease>> RunAsync()
        {
            var tasks = new List<Task>();
            var itens = new List<ItemLiberacaoRelease>();

            //addionar tramanento de excessão
            foreach (var projeto in Projetos)
            {
                tasks.Add(Task.Run(() =>
                {
                    var itemLiberacaoRelease = AnalisarItemLiberacaoRelease(projeto);

                    itens.Add(itemLiberacaoRelease);

                    AddNotifications(itemLiberacaoRelease.Notifications);
                }));
            }

            await Task.WhenAll(tasks);

            return itens;
        }

        private ItemLiberacaoRelease AnalisarItemLiberacaoRelease(Projeto projeto)
        {
            var itemLiberacaoRelease = new ItemLiberacaoRelease(projeto);

            var pathRelease = $@"{Versao.DiretorioRelease}\{projeto.Path}";
            var pathTeste = $@"{Versao.DiretorioTeste}\{projeto.Path}";

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