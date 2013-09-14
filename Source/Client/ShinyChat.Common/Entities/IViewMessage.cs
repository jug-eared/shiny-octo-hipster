using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IViewMessage
    {
        /// <summary>
        /// Gets or sets the content of the message
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets the user the message was sent by
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// Gets or sets the timestamp the message was received from server
        /// </summary>
        DateTime Timestamp { get; set; }
    }
}
