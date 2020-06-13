using System;

namespace ControleDeRelease.Domain.Fake
{
    public class DadosVersaoFake
    {
        public string Release { get; private set; }

        public DateTime DataVersao { get; private set; }

        public DadosVersaoFake(string release, DateTime dataVersao)
        {
            Release = release;
            DataVersao = dataVersao;
        }
    }
}
