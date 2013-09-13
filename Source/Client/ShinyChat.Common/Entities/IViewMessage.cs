using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IViewMessage
    {
        string Message { get; set; }
        string User { get; set; }
        DateTime Timestamp { get; set; }
    }
}
