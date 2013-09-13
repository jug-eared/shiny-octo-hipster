using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common.Entities;

namespace ShinyChat.Common.Managers
{
    public interface IMessageBuilder
    {
        IServerMessage BuildCommand(uint lastMessageId, Enums.CommandType commandType, IChannel channel, string user);
        IServerMessage BuildServerMessage(uint lastMessageId, IChannel channel, string user);
    }
}
