using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Enums;
using System;

namespace ControleDeRelease.Domain.Fake
{
    public class ItemLiberacaoReleaseFake
    {
        public Projeto Projeto { get; set; }

        public DadosVersaoFake DadosVersaoRelease { get; set; }

        public DadosVersaoFake DadosVersaoTeste { get; set; }

        public StatusRelease StatusAtualizacao { get; private set; }

        public ItemLiberacaoReleaseFake(Projeto projeto) => Projeto = projeto;

        public void SetStatusRelease()
        {
            Version fileVersionRelease = new Version(DadosVersaoRelease.Release);
            Version fileVersionTeste = new Version(DadosVersaoTeste.Release);

            StatusAtualizacao = fileVersionTeste > fileVersionRelease ? StatusRelease.Atualizado : StatusRelease.Mantido;
        }
    }
}
