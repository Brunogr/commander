using Commander.Abstractions;
using Flunt.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commander.Core
{
    public abstract class Command : Notifiable, ICommand<CommandResult>
    {
        public Command()
        {
            MessageType = GetType().Name;
            Timestamp = DateTimeOffset.Now;
        }
        public string MessageType { get; }

        public DateTimeOffset Timestamp { get; }

        [JsonIgnore]
        public bool AfterHandle { get; set; } = true;

        public abstract void Validate();
    }
}
