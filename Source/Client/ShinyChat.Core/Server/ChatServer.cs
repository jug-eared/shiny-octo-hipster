using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common;
using System.Net;

namespace ShinyChat.Core.Server
{
    public class ChatServer : IChatServer
    {
        public int Port
        {
            get;
            set;
        }

        public IPAddress Ip
        {
            get;
            set;
        }

        public IPEndPoint Endpoint
        {
            get;
            set;
        }

        public AsyncCallback MessageReceived
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool StartListen()
        {
            throw new NotImplementedException();
        }

        public bool StopListen()
        {
            throw new NotImplementedException();
        }


        public bool OpenConnection()
        {
            throw new NotImplementedException();
        }

        public bool CloseConnection()
        {
            throw new NotImplementedException();
        }
    }
}
