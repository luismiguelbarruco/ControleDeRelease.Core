using ControleDeRelease.Domain.Enums;
using Flunt.Notifications;
using System;
using System.IO;

namespace ControleDeRelease.Domain.Entities
{
    public class ItemLiberacaoRelease : Notifiable
    {
        public Projeto Projeto { get; private set; }

        public ReleaseAttributes ReleaseAttriburesDiretorioRelese { get; set; }

        public ReleaseAttributes ReleaseAttriburesDiretorioTeste { get; set; }

        public StatusRelease StatusAtualizacao { get; private set; } = StatusRelease.NaoVerificado;

        public ItemLiberacaoRelease() { }

        public ItemLiberacaoRelease(Projeto projeto) => Projeto = projeto;

        public bool Validate(string path)
        {
            if (!File.Exists(path))
            {
                AddNotification(nameof(Projeto), $"Arquivo {Projeto} não encontrado na pasta: {path}");
                return false;
            }

            return true;
        }

        public void SetStatusRelease()
        {
            Version fileVersionRelease = new Version(ReleaseAttriburesDiretorioRelese.Release);
            Version fileVersionTeste = new Version(ReleaseAttriburesDiretorioTeste.Release);

            StatusAtualizacao = fileVersionTeste > fileVersionRelease ? StatusRelease.Atualizado : StatusRelease.Mantido;
        }
    }
}
