using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public partial class Enums
    {
        /// <summary>
        /// Gets the MessageType of a message that is sent or received from server
        /// </summary>
        public enum MessageType
        {
            Undefined = 0,
            Message = 1,
            Command = 2,
            Response = 3,
        }
    }
}
