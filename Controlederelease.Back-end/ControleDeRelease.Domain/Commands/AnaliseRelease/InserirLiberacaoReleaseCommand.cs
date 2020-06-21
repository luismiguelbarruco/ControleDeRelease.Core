
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.VireModel;
using Flunt.Validations;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.Commands.AnaliseRelease
{
    public class InserirLiberacaoReleaseCommand : CommandBase
    {
        public Versao Versao { get; set; }
        public List<ItemLiberacaoReleaseViewModel> Itens { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNull(Versao, nameof(Versao), "Versão não pode ser nulo")
                .IsNotNull(Itens, nameof(Itens), "Itens não podem ser nulo")
                .IsNotNullOrEmpty(Versao.Nome, nameof(Versao.Nome), "Nome da versão não pode ser vazio")
                .AreNotEquals(0, Versao.Id, nameof(Versao.Id), "Id da versão não pode ser zero")
            );
            //Adicionar contrato personalizado: verificar se lista de itens contem valor zero na propriedade id
        }
    }
}
