using ControleDeRelease.Domain.ViewModel;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.Entities
{
    public class ItensLiberacaoRelease
    {
        public List<ItemLiberacaoRelease> Parse(List<ItemLiberacaoReleaseViewModel> itensLiberacaoReleaseViewModel)
        {
            var itensLiberacaoRelease = new List<ItemLiberacaoRelease>();

            foreach (var item in itensLiberacaoReleaseViewModel)
            {
                var itemLiberacaoRelease = new ItemLiberacaoRelease().Parse(item);
                itensLiberacaoRelease.Add(itemLiberacaoRelease);
            }

            return itensLiberacaoRelease;
        }
    }
}
