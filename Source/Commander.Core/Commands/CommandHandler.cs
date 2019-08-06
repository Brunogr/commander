using Commander.Abstractions;
using Commander.Core.Base;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commander.Core
{
    public abstract class CommandHandler<TCommand> : HandlerBase, 
        ICommandHandler<TCommand> where TCommand : ICommand<CommandResult>
    {
        protected IHandler handler;
        public CommandHandler(IHandler handler) : base(handler)
        {
            events = new List<IEvent>();
            this.handler = handler;
        }

        public async Task<CommandResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            request.Validate();

            if (request.Invalid)
            {
                await Notify(request.Notifications);
                return new CommandResult(false);
            }

            var result = await HandleCommandAsync(request);

            //if result failed, but command handler is valid, then add a notification if any message was sent to CommandResult
            if (!result.Success && Valid && !string.IsNullOrWhiteSpace(result.Message))
                AddNotification("CommandResult.Failed", result.Message);

            await Notify(Notifications);

            await RaiseEvents();

            if (request.AfterHandle)
                await AfterHandleAsync();

            return result;
        }

        protected Task AddEventsAsync(params IEvent[] @event)
        {
            events.AddRange(@event);

            return Task.CompletedTask;
        }

        public abstract Task<CommandResult> HandleCommandAsync(TCommand @command);

        public abstract Task AfterHandleAsync();
    }
}
