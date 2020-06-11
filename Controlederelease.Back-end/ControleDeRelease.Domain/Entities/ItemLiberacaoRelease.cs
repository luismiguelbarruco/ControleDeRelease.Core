using ControleDeRelease.Domain.Enums;
using ControleDeRelease.Domain.Extensions;
using Flunt.Notifications;
using System.IO;

namespace ControleDeRelease.Domain.Entities
{
    public class ItemLiberacaoRelease : Notifiable
    {
        public string Projeto { get; set; }
        public AttributeFileRelease AttributeFileRelease { get; set; }
        public AttributeFileTeste AttributeFileTeste { get; set; }
        public StatusRelease StatusAtualizacao { get; set; }
        public string StatusAtualizacaoDescription => StatusAtualizacao.GetDescriptionAttribute();

        public ItemLiberacaoRelease(string projeto)
        {
            Projeto = projeto;
        }

        public bool Validate(string path)
        {
            if (!File.Exists(path))
            {
                AddNotification(nameof(Projeto), $"Arquivo {Projeto} não encontrado na pasta: {path}");
                return false;
            }

            return true;
        }
    }
}
