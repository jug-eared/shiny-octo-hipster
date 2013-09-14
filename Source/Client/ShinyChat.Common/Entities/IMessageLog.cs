using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IMessageLog
    {
        /// <summary>
        /// Gets or sets an enumerable of messages in the message log
        /// </summary>
        IEnumerable<IViewMessage> Messages { get; set; }
    }
}
