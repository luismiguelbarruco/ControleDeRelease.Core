using Flunt.Notifications;

namespace ControleDeRelease.Domain.Commands
{
    public abstract class CommandBase : Notifiable, ICommand
    {
        public abstract void Validate();
    }
}