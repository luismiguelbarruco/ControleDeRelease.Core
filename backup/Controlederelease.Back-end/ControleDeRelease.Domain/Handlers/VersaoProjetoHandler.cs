
using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.VersaoProjeto;
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Queries;
using ControleDeRelease.Domain.Repository;
using Flunt.Notifications;

namespace ControleDeRelease.Domain.Handlers
{
    public class VersaoProjetoHandler : Notifiable, IHandle<InserirVersaoProjetoCommand>, IHandle<AtualizarVersaoProjetoCommand>, IHandle<DeletarVersaoProjetoCommand>
    {
        private readonly IVersaoProjetoRepository _versaoProjetoRepository;

        public VersaoProjetoHandler(IVersaoProjetoRepository diretorioProjetoRepository) => _versaoProjetoRepository = diretorioProjetoRepository;

        public ICommandResult Handler(InserirVersaoProjetoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel cadastrar a versão.", command.Notifications);
            }

            var query = VersaoQueries.Selecionar(command.Nome);

            var result = _versaoProjetoRepository.Selecionar(query);

            if (result != null)
                return new CommandResult(false, "Já existe um versão com mesmo nome cadastrado.");

            var versao = new Versao
            {
                Nome = command.Nome,
                DiretorioRelease = command.DiretorioRelease,
                DiretorioTeste = command.DiretorioTeste,
            };

            if (!_versaoProjetoRepository.Cadastrar(versao))
                return new CommandResult(false, "Ocorreu erro ao cadastrar a versão.");

            return new CommandResult(true, "Versão cadastrado com sucesso.");
        }

        public ICommandResult Handler(AtualizarVersaoProjetoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel atualizar a versão.", command.Notifications);
            }

            var query = VersaoQueries.Selecionar(command.Id);

            var result = _versaoProjetoRepository.Selecionar(query);

            if (result == null)
                return new CommandResult(false, "Projeto não encontrado.");

            query = VersaoQueries.Selecionar(command.Nome);

            result = _versaoProjetoRepository.Selecionar(query);

            if (result != null)
                return new CommandResult(false, "Já existe um versão com mesmo nome cadastrado.");

            var versao = new Versao
            {
                Id = command.Id,
                Nome = command.Nome,
                DiretorioRelease = command.DiretorioRelease,
                DiretorioTeste = command.DiretorioTeste,
            };

            if(!_versaoProjetoRepository.Atualizar(versao))
                return new CommandResult(false, "Ocorreu erro ao atualizar a versão.");

            return new CommandResult(true, "Versão atualizado com sucesso.");
        }

        public ICommandResult Handler(DeletarVersaoProjetoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel deletar a versão.", command.Notifications);
            }

            var query = VersaoQueries.Selecionar(command.Id);

            var result = _versaoProjetoRepository.Selecionar(query);

            if(result ==  null)
                return new CommandResult(false, "Versão não encontrado.");

            if(!_versaoProjetoRepository.Deletar(command.Id))
                return new CommandResult(false, "Ocorreu erro ao deletar versão.");

            return new CommandResult(true, "Versão deletado com sucesso.");
        }
    }
}
