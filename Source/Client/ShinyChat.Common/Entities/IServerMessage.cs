using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IServerMessage
    {
        /// <summary>
        /// Gets or sets the bytesize of the options block of the message
        /// </summary>
        uint OptionsSize { get; set; }

        /// <summary>
        /// Gets or sets the bytesize of the contents block of the message
        /// </summary>
        uint ContentSize { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the message to match outgoing requests and incoming responses
        /// </summary>
        uint Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ShinyChat.Common.Entities.Enums.MessageType"/> of the Message
        /// </summary>
        Enums.MessageType MessageType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ShinyChat.Common.Entities.Enums.CommandType"/> of the Message
        /// </summary>
        Enums.CommandType CommandType { get; set; }

        /// <summary>
        /// Gets or sets the channel the message belongs to
        /// </summary>
        IChannel Channel { get; set; }

        /// <summary>
        /// Gets or sets the user the message is sent by
        /// </summary>
        string User { get; set; }

        /// <summary>
        /// Gets or sets the actual message content of the message
        /// </summary>
        string MessageContent { get; set; }

        /// <summary>
        /// Gets or sets the resulting byte array of the message, that is send to the server
        /// </summary>
        byte[] SerializedMessage { get; set; }
    }
}
