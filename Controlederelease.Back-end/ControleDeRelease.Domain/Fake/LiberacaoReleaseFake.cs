using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Helpers;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.Fake
{
    public class LiberacaoReleaseFake
    {
        public List<ItemLiberacaoReleaseFake> Items { get; set; }

        public LiberacaoReleaseFake()
        {
            Items = new List<ItemLiberacaoReleaseFake>();
        }

        public void AnalisarReleasesFake(Versao versao, List<Projeto> projetos)
        {
            var count = 0;

            projetos.ForEach(projeto =>
            {
                var pathRelease = $@"{versao.DiretorioRelease}\{projeto.Path}";
                var pathTeste = $@"{versao.DiretorioTeste}\{projeto.Path}";

                var itemLiberacaoReleaseFake = new ItemLiberacaoReleaseFake(projeto);

                itemLiberacaoReleaseFake.DadosVersaoRelease = FileInfoHelper.GetDataFileVersionFake();
                itemLiberacaoReleaseFake.DadosVersaoTeste = FileInfoHelper.GetDataFileVersionFake();

                itemLiberacaoReleaseFake.SetStatusRelease();

                count++;

                Items.Add(itemLiberacaoReleaseFake);
            });
        }
    }
}
