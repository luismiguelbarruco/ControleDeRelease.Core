
using Flunt.Validations;

namespace ControleDeRelease.Domain.Commands.Projeto
{
    public class DeletarProjetoCommand : CommandBase
    {
        public int Id { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(0, Id, nameof(Id), "Informe um id válido")
            );
        }
    }
}
