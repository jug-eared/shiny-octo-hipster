using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IServerMessage
    {
        uint OptionsSize { get; set; }
        uint ContentSize { get; set; }
        uint Id { get; set; }
        Enums.MessageType MessageType { get; set; }
        Enums.CommandType CommandType { get; set; }
        IChannel Channel { get; set; }
        string User { get; set; }
        string MessageContent { get; set; }

        byte[] SerializedMessage { get; set; }
    }
}
