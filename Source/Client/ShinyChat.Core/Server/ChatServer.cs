using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common;
using System.Net;
using System.Net.Sockets;
using ShinyChat.Common.Server;

namespace ShinyChat.Core.Server
{
    public class ChatServer : IChatServer
    {
        public ChatServer()
        {
            _client = new TcpClient();
            _subscribers = new List<IServerSubscriber>();
        }

        private TcpClient _client;
        private List<IServerSubscriber> _subscribers;

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
            get;
            set;
        }

        public bool OpenConnection()
        {
            if (Endpoint != null && _client != null)
            {
                try
                {
                    _client.Connect(Endpoint);
                }
                catch (Exception ex)
                {
                    return false;
                    // TODO Log Error
                }
                return true;
            }
            return false;
        }

        public bool CloseConnection()
        {
            if (_client != null && _client.Connected)
            {
                try
                {
                    _client.Close();
                }
                catch (Exception ex)
                {
                    return false;
                    // TODO Log Error
                }
                return true;
            }
            return false;
        }

        private void SocketListener()
        {
            // TODO If message then notify subscribers
        }

        public void Subscribe(IServerSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(IServerSubscriber subscriber)
        {
            if (_subscribers.Contains(subscriber))
                _subscribers.Remove(subscriber);
        }
    }
}
