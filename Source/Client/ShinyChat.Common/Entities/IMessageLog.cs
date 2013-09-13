using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IMessageLog
    {
        List<IViewMessage> Messages { get; set; }
    }
}
