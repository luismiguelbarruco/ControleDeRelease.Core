using ControleDeRelease.Domain.Entities;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.VireModel
{
    public class ItensLiberacaoReleaseViewModel
    {
        public List<ItemLiberacaoReleaseViewModel> Parse(List<ItemLiberacaoRelease> itensLiberacaoRelease)
        {
            var itensLiberacaoReleaseVireModel = new List<ItemLiberacaoReleaseViewModel>();

            foreach (var item in itensLiberacaoRelease)
            {
                var itemLiberacaoReleaseVireModel = new ItemLiberacaoReleaseViewModel().Parse(item);
                itensLiberacaoReleaseVireModel.Add(itemLiberacaoReleaseVireModel);
            }

            return itensLiberacaoReleaseVireModel;
        }
    }
}
