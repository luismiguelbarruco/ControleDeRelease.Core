using ControleDeRelease.Domain.Entities;
using Flunt.Validations;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.Commands.Projeto
{
    public class InserirProjetoCommand : CommandBase
    {
        public string Nome { get; set; }
        public string SubpastaRelease { get; set; }
        public string SubpastaTeste { get; set; }
        public List<Versao> Versoes { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome do projeto não pode ser vazio")
                .AreNotEquals(0, Versoes.Count, nameof(Versoes), "Nenhuma versão informada para o projeto")
            );
        }
    }
}
