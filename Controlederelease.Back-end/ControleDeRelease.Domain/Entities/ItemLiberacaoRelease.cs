using ControleDeRelease.Domain.Enums;
using ControleDeRelease.Domain.Extensions;
using Flunt.Notifications;
using System.IO;

namespace ControleDeRelease.Domain.Entities
{
    public class ItemLiberacaoRelease : Notifiable
    {
        public string Projeto { get; set; }

        public AttributeFile AttributeFileRelease { get; set; }

        public AttributeFile AttributeFileTeste { get; set; }

        public StatusRelease StatusAtualizacao { get; private set; }

        public string StatusAtualizacaoDescription => StatusAtualizacao.GetDescriptionAttribute();

        public ItemLiberacaoRelease(string projeto) => Projeto = projeto;

        public bool Validate(string path)
        {
            if (!File.Exists(path))
            {
                AddNotification(nameof(Projeto), $"Arquivo {Projeto} não encontrado na pasta: {path}");
                return false;
            }

            return true;
        }

        public void SetStatusRelease(StatusRelease statusRelease)
        {
            StatusAtualizacao = statusRelease;
        }
    }
}
