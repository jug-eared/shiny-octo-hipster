using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IChannel
    {
        /// <summary>
        /// Gets or sets the unique name of the channel
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the messagelog containing all previous messages received from server
        /// </summary>
        IMessageLog MessageLog { get; set; }

        /// <summary>
        /// Gets or sets a boolean value determining wether the channel is actually opened in client or not
        /// </summary>
        bool IsOpened { get; set; }
    }
}
