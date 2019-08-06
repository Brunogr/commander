using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commander.Abstractions
{
    public interface IValidatorHandler<TMessage> : INotificationHandler<TMessage>
        where TMessage : IValidatable
    {
    }
}
