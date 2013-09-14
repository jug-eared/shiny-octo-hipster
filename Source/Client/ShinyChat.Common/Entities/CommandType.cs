using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public partial class Enums 
    {
        /// <summary>
        /// Gets the CommandType of a command message that is sent or received from server
        /// </summary>
        public enum CommandType
        {
            Undefined = 0,
            JoinChannel = 1,
            LeaveChannel = 2,
            GetChannels = 3,
        }
    }
}
