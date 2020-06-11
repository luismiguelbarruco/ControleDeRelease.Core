﻿
using Flunt.Validations;

namespace ControleDeRelease.Domain.Commands.Projeto
{
    public class AtualizarProjetoCommand : CommandBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Subpasta { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(0, Id, nameof(Id), "Informe um id válido")
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome do projeto não pode ser vazio")
            );
        }
    }
}
