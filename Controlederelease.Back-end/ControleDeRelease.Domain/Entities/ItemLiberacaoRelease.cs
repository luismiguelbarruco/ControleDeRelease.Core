using ControleDeRelease.Domain.Enums;
using Flunt.Notifications;
using System;
using System.IO;

namespace ControleDeRelease.Domain.Entities
{
    public class ItemLiberacaoRelease : Notifiable
    {
        public Projeto Projeto { get; set; }

        public DadosVersao DadosVersaoRelease { get; set; }

        public DadosVersao DadosVersaoTeste { get; set; }

        public StatusRelease StatusAtualizacao { get; private set; } = StatusRelease.NaoVerificado;

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
            Version fileVersionRelease = new Version(DadosVersaoRelease.Release);
            Version fileVersionTeste = new Version(DadosVersaoTeste.Release);

            StatusAtualizacao = fileVersionTeste > fileVersionRelease ? StatusRelease.Atualizado : StatusRelease.Mantido;
        }
    }
}
