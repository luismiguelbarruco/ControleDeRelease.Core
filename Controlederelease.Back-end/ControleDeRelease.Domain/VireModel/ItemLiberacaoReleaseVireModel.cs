
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Extensions;
using System;

namespace ControleDeRelease.Domain.VireModel
{
    public class ItemLiberacaoReleaseVireModel
    {
        public int Id { get; private set; }
        public string Projeto { get; private set; }
        public string VersaoRelease { get; private set; }
        public DateTime DataVersaoRelease { get; private set; }
        public string VersaoTeste { get; private set; }
        public DateTime DataVersaoTeste { get; private set; }
        public string Status { get; private set; }

        public ItemLiberacaoReleaseVireModel Parse(ItemLiberacaoRelease itemLiberacaoRelease)
        {
            return new ItemLiberacaoReleaseVireModel
            {
                Id = itemLiberacaoRelease.Projeto.Id,
                Projeto = itemLiberacaoRelease.Projeto.Nome,
                VersaoRelease = itemLiberacaoRelease.DadosVersaoRelease.Release,
                DataVersaoRelease = itemLiberacaoRelease.DadosVersaoRelease.DataVersao,
                VersaoTeste = itemLiberacaoRelease.DadosVersaoTeste.Release,
                DataVersaoTeste = itemLiberacaoRelease.DadosVersaoRelease.DataVersao,
                Status = itemLiberacaoRelease.StatusAtualizacao.GetDescriptionAttribute()
            };
        }
    }
}
