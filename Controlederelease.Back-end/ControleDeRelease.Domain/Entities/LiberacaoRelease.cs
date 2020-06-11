using Flunt.Notifications;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.Entities
{
    public class LiberacaoRelease : Notifiable
    {
        public List<ItemLiberacaoRelease> Items { get; set; }

        public LiberacaoRelease()
        {
            Items = new List<ItemLiberacaoRelease>();
        }
    }
}
