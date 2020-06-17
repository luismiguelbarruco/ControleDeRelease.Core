
using Flunt.Validations;

namespace ControleDeRelease.Domain.Commands.VersaoProjeto
{
    public class AtualizarVersaoProjetoCommand : CommandBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DiretorioRelease { get; set; }
        public string DiretorioTeste { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .AreNotEquals(0, Id, nameof(Id), "Informe um id válido")
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome do projeto não informado")
                .IsNotNullOrEmpty(DiretorioRelease, nameof(DiretorioRelease), "Diretorio release não informado.")
                .IsNotNullOrEmpty(DiretorioTeste, nameof(DiretorioTeste), "Diretorio teste não informado.")
                .AreNotEquals(DiretorioRelease, DiretorioTeste, DiretorioTeste, "Diretorio release não pode ser o mesmo do diretorio teste.")
            );
        }
    }
}
