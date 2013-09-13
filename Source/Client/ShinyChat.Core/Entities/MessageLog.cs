using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common.Entities;

namespace ShinyChat.Core.Entities
{
    public class MessageLog : IMessageLog
    {
        public List<IViewMessage> Messages
        {
            get;
            set;
        }
    }
}
