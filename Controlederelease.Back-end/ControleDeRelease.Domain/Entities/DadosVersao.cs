using System;

namespace ControleDeRelease.Domain.Entities
{
    public class DadosVersao
    {
        public string Release { get; private set; }

        public DateTime DataVersao { get; private set; }

        public DadosVersao(string release, DateTime dataVersao)
        {
            Release = release;
            DataVersao = dataVersao;
        }
    }
}
