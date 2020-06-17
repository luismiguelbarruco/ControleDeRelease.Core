﻿
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Extensions;
using System;

namespace ControleDeRelease.Domain.VireModel
{
    public class ItemLiberacaoReleaseVireModel
    {
        public int Id { get; set; }

        public string Projeto { get; set; }

        public string VersaoRelease { get; set; }

        public DateTime DataVersaoRelease { get; set; }

        public string VersaoTeste { get; set; }

        public DateTime DataVersaoTeste { get; set; }

        public string Status { get; set; }

        public ItemLiberacaoReleaseVireModel Parse(ItemLiberacaoRelease itemLiberacaoRelease)
        {
            return new ItemLiberacaoReleaseVireModel
            {
                Id = itemLiberacaoRelease.Projeto.Id,
                Projeto = itemLiberacaoRelease.Projeto.Nome,
                VersaoRelease = itemLiberacaoRelease.ReleaseAttriburesDiretorioRelese.Release,
                DataVersaoRelease = itemLiberacaoRelease.ReleaseAttriburesDiretorioRelese.DataVersao,
                VersaoTeste = itemLiberacaoRelease.ReleaseAttriburesDiretorioTeste.Release,
                DataVersaoTeste = itemLiberacaoRelease.ReleaseAttriburesDiretorioTeste.DataVersao,
                Status = itemLiberacaoRelease.StatusAtualizacao.GetDescriptionAttribute()
            };
        }
    }
}
