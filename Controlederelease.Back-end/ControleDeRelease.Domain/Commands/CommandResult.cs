using Flunt.Notifications;
using System.Collections.Generic;

namespace ControleDeRelease.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public bool Sucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public CommandResult(){ }

        public CommandResult(bool sucess, object data)
        {
            Sucess = sucess;
            Data = data;
        }

        public CommandResult(bool sucess, string message)
        {
            Sucess = sucess;
            Message = message;
        }

        public CommandResult(bool sucess, string message, IReadOnlyCollection<Notification> notifications)
            : this(sucess, notifications)
        {
            Sucess = sucess;
            Message = message;
        }
    }
}
