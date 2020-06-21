using System;

namespace ControleDeRelease.Domain.Entities
{
    public class ReleaseAttributes
    {
        public string Release { get; private set; } = string.Empty;

        public DateTime DataVersao { get; private set; } = DateTime.Now;

        public ReleaseAttributes(string release, DateTime dataVersao)
        {
            Release = release;
            DataVersao = dataVersao;
        }
    }
}
