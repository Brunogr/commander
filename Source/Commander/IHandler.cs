using Commander.Abstractions.Bases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Abstractions
{
    public interface IHandler
    {
        Task<CommandResult> Send<TCommand>(TCommand command)
            where TCommand : ICommand<CommandResult>;

        Task RaiseEvent<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
