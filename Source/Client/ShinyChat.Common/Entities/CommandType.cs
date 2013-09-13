using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public partial class Enums 
    {
        public enum CommandType
        {
            Undefined = 0,
            JoinChannel = 1,
            LeaveChannel = 2,
            GetChannels = 3,
        }
    }
}
