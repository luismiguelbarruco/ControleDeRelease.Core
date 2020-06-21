using ControleDeRelease.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.VireModel
{
    public class LibercaoReleaseViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<ItemLiberacaoReleaseViewModel> Items { get; set; }

        public LibercaoReleaseViewModel Parse(LiberacaoRelease liberacaoRelease)
        {
            var resulParse = new LibercaoReleaseViewModel
            {
                Id = liberacaoRelease.Id,
                Data = liberacaoRelease.Data,
                Items = new ItensLiberacaoReleaseViewModel().Parse(liberacaoRelease.Itens)
            };

            return resulParse;
        }
    }
}
