using ControleDeRelease.Domain.Enums;
using System;

namespace ControleDeRelease.Domain.Entities
{
    public class HistoricoLiberacaoRelease
    {
        public int Id { get; set; }
        public string Projeto { get; set; }
        public InformacoesRelease VersaoRelease { get; set; }
        public InformacoesRelease VersaoTeste { get; set; }
        public StatusRelease StatusAtualizacao { get; set; }
    }

    public class InformacoesRelease
    {
        public string Versao { get; set; }
        public DateTime Data { get; set; }
    }
}
