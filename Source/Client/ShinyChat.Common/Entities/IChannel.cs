using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IChannel
    {
        string Name { get; set; }
        IMessageLog MessageLog { get; set; }
        bool IsOpened { get; set; }
    }
}
