﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commander.Abstractions
{
    public interface IMessage
    {
        [JsonIgnore]
        string MessageType {get; }
        DateTimeOffset Timestamp { get; }
    }
}
