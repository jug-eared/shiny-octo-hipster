using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public partial class Enums
    {
        public enum MessageType
        {
            Undefined = 0,
            Message = 1,
            Command = 2,
            Response = 3,
        }
    }
}
