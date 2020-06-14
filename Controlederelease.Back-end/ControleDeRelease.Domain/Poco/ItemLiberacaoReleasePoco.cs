using ControleDeRelease.Domain.Enums;
using ControleDeRelease.Domain.Extensions;
using ControleDeRelease.Domain.VireModel;
using System;

namespace ControleDeRelease.Domain.Poco
{
    public class ItemLiberacaoReleasePoco
    {
        public int Id { get; set; } = 0;
        public string Nome { get; set; } = string.Empty;
        public string VersaoRelease { get; set; } = string.Empty;
        public DateTime DataVersaoRelease { get; set; } = DateTime.Now;
        public string VersaoTeste { get; set; } = string.Empty;
        public DateTime DataVersaoTeste { get; set; } = DateTime.Now;
        public string StatusAtualizacao { get; set; } = StatusRelease.NaoVerificado.GetDescriptionAttribute();

        public ItemLiberacaoReleasePoco Parse(ItemLiberacaoReleaseVireModel itemLiberacaoReleaseVireModel)
        {
            return new ItemLiberacaoReleasePoco
            {
                Id = itemLiberacaoReleaseVireModel.Id,
                Nome = itemLiberacaoReleaseVireModel.Projeto,
                VersaoRelease = itemLiberacaoReleaseVireModel.VersaoRelease,
                DataVersaoRelease = itemLiberacaoReleaseVireModel.DataVersaoRelease,
                VersaoTeste = itemLiberacaoReleaseVireModel.VersaoTeste,
                DataVersaoTeste = itemLiberacaoReleaseVireModel.DataVersaoTeste,
                StatusAtualizacao = itemLiberacaoReleaseVireModel.Status
            };
        }
    }
}
