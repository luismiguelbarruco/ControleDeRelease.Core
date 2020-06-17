using ControleDeRelease.Domain.Commands;
using ControleDeRelease.Domain.Commands.Projeto;
using ControleDeRelease.Domain.Entities;
using ControleDeRelease.Domain.Queries;
using ControleDeRelease.Domain.Repository;
using Flunt.Notifications;

namespace ControleDeRelease.Domain.Handlers
{
    public class ProjetoHandler : Notifiable, IHandle<InserirProjetoCommand>, IHandle<AtualizarProjetoCommand>, IHandle<DeletarProjetoCommand>
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoHandler(IProjetoRepository projetoRepository) => _projetoRepository = projetoRepository;

        public ICommandResult Handler(InserirProjetoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel cadastrar o projeto.", command.Notifications);
            }

            var query = ProjetoQueries.Selecionar(command.Nome);

            var result  = _projetoRepository.Selecionar(query);

            if(result != null)
                return new CommandResult(false, "Já existe um projero com mesmo nome cadastrado.");

            var projeto = new Projeto
            {
                Nome = command.Nome,
                Subpasta = command.Subpasta,
                Versoes = command.Versoes
            };

            if(!_projetoRepository.Cadastrar(projeto))
                return new CommandResult(false, "Ocorreu erro ao cadastrar o projeto.");

            return new CommandResult(true, "Projeto cadastrado com sucesso.");
        }

        public ICommandResult Handler(AtualizarProjetoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel atualizar o projeto.", command.Notifications);
            }

            var query = ProjetoQueries.Selecionar(command.Id);

            var result = _projetoRepository.Selecionar(query);

            if (result == null)
                return new CommandResult(false, "Projeto não encontrado.");

            query = ProjetoQueries.Selecionar(command.Id, command.Nome);

            result = _projetoRepository.Selecionar(query);

            if(result != null)
                return new CommandResult(false, "Já existe um projeto com mesmo nome cadastrado.");

            var projeto = new Projeto
            {
                Id = command.Id,
                Nome = command.Nome,
                Subpasta = command.Subpasta,
                Versoes = command.Versoes
            };

            if (!_projetoRepository.Atualizar(projeto))
                return new CommandResult(false, "Ocorreu erro ao atualizar o projeto.");

            return new CommandResult(true, "Projeto atualizado com sucesso.");
        }

        public ICommandResult Handler(DeletarProjetoCommand command)
        {
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel deletar o projeto.", command.Notifications);
            }

            var query = ProjetoQueries.Selecionar(command.Id);

            var result = _projetoRepository.Selecionar(query);

            if (result == null)
                return new CommandResult(false, "Projeto não encontrado.");

            if (!_projetoRepository.Deletar(command.Id))
                return new CommandResult(false, "Ocorreu erro ao deletar o projeto.");

            return new CommandResult(true, "Projeto deletado com sucesso.");
        }
    }
}
