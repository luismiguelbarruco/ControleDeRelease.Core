using ControleDeRelease.Domain.Enums;
using ControleDeRelease.Domain.Extensions;
using ControleDeRelease.Domain.ViewModel;
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
                AddNotification(nameof(Projeto), $"Arquivo {Projeto.Nome} não encontrado na pasta: {path}");
                return false;
            }

            return true;
        }

        public void SetStatusRelease()
        {
            Version fileVersionRelease = new Version(ReleaseAttriburesDiretorioRelese.Release);
            Version fileVersionTeste = new Version(ReleaseAttriburesDiretorioTeste.Release);

            if (fileVersionTeste > fileVersionRelease)
                StatusAtualizacao = StatusRelease.Atualizado;

            else if (fileVersionTeste == fileVersionRelease 
                && ReleaseAttriburesDiretorioTeste.DataVersao > ReleaseAttriburesDiretorioRelese.DataVersao)
                StatusAtualizacao = StatusRelease.Atualizado;

            else
                StatusAtualizacao = StatusRelease.Mantido;
        }

        public ItemLiberacaoRelease Parse(ItemLiberacaoReleaseViewModel itemLiberacaoReleaseVireModel)
        {
            var projeto = new Projeto
            {
                Id = itemLiberacaoReleaseVireModel.Id,
                Nome = itemLiberacaoReleaseVireModel.Projeto
            };

            var releaseAttriburesDiretorioRelese = new ReleaseAttributes(
                itemLiberacaoReleaseVireModel.VersaoRelease, 
                itemLiberacaoReleaseVireModel.DataVersaoRelease
            );

            var releaseAttriburesDiretorioTeste = new ReleaseAttributes(
                itemLiberacaoReleaseVireModel.VersaoTeste,
                itemLiberacaoReleaseVireModel.DataVersaoTeste
            );

            var item = new ItemLiberacaoRelease(projeto)
            {
                ReleaseAttriburesDiretorioRelese = releaseAttriburesDiretorioRelese,
                ReleaseAttriburesDiretorioTeste = releaseAttriburesDiretorioTeste,
                StatusAtualizacao = itemLiberacaoReleaseVireModel.Status.GetValueFromDescription<StatusRelease>()
            };

            return item;
        }
    }
}
