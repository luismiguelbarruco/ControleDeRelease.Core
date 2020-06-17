
using Flunt.Validations;

namespace ControleDeRelease.Domain.Commands.VersaoProjeto
{
    public class InserirVersaoProjetoCommand : CommandBase
    {
        public string Nome { get; set; }
        public string DiretorioRelease { get; set; }
        public string DiretorioTeste { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(Nome, nameof(Nome), "Nome do projeto não informado")
                .IsNotNullOrEmpty(DiretorioRelease, nameof(DiretorioRelease), "Diretorio release não informado.")
                .IsNotNullOrEmpty(DiretorioTeste, nameof(DiretorioTeste), "Diretorio teste não informado.")
                .AreNotEquals(DiretorioRelease, DiretorioTeste, DiretorioTeste, "Diretorio release não pode ser o mesmo do diretorio teste.")
            );
        }
    }
}
