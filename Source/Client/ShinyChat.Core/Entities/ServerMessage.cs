using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public class ServerMessage : IServerMessage
    {
        public uint OptionsSize
        {
            get;
            set;
        }

        public uint ContentSize
        {
            get;
            set;
        }

        public uint Id
        {
            get;
            set;
        }

        public Enums.MessageType MessageType
        {
            get;
            set;
        }

        public Enums.CommandType CommandType
        {
            get;
            set;
        }

        public IChannel Channel
        {
            get;
            set;
        }

        public string User
        {
            get;
            set;
        }

        public string MessageContent
        {
            get;
            set;
        }

        public byte[] SerializedMessage
        {
            get;
            set;
        }
    }
}
