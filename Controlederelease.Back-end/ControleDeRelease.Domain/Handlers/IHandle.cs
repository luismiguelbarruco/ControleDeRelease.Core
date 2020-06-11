
using ControleDeRelease.Domain.Commands;

namespace ControleDeRelease.Domain.Handlers
{
    public interface IHandle<T> where T : Commands.ICommand
    {
        ICommandResult Handler (T command);
    }
}
