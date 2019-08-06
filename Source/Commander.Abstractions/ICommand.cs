using Flunt.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Abstractions
{
    public interface ICommand<TCommandResult> : IRequest<TCommandResult>, 
        IMessage where TCommandResult : ICommandResult
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool Valid { get; }
        bool Invalid { get; }
        bool AfterHandle { get; set; }
        void Validate();
    }
}
