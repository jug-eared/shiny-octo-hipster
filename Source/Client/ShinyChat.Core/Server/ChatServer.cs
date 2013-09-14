using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common;
using System.Net;
using System.Net.Sockets;
using ShinyChat.Common.Server;
using System.Threading;
using ShinyChat.Common.Entities;
using System.IO;
using System.Xml;

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
        private bool _connectionActive;

        public IEnumerable<IServerMessage> IncomingMessages
        {
            get;
            set;
        }

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

        public bool OpenConnection()
        {
            if (Endpoint != null && _client != null)
            {
                try
                {
                    _client.Connect(Endpoint);
                    _connectionActive = true;
                    IncomingMessages = new List<IServerMessage>();
                    _subscribers = new List<IServerSubscriber>();
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
                    _connectionActive = false;
                    IncomingMessages = null;
                    _subscribers = null;
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
            while (_connectionActive && _client.Connected)
            {
                Thread.Sleep(100);
                if (_client.GetStream().DataAvailable)
                {
                    // Read Data from stream
                    var optionsSizeBuffer = new byte[4];
                    var contentSizeBuffer = new byte[4];

                    _client.GetStream().Read(optionsSizeBuffer, 0, 4);
                    _client.GetStream().Read(contentSizeBuffer, 0, 4);

                    var optionsSize = Convert.ToUInt32(optionsSizeBuffer);
                    var contentSize = Convert.ToUInt32(contentSizeBuffer);

                    var optionsBuffer = new byte[optionsSize];
                    var contentBuffer = new byte[contentSize];

                    // Dirty typecast (OH NOEZ)
                    _client.GetStream().Read(optionsBuffer, 0, (int)optionsSize);
                    _client.GetStream().Read(contentBuffer, 0, (int)contentSize);

                    // TODO Convert Data to IServerMessage
                    // -- -- -- XML Reader
                    var optionsText = System.Text.Encoding.UTF8.GetString(optionsBuffer);
                    var contentText = System.Text.Encoding.UTF8.GetString(contentBuffer);

                    var optionsDoc = new XmlDocument();
                    optionsDoc.LoadXml(optionsText);
                    var contentDoc = new XmlDocument();
                    contentDoc.LoadXml(contentText);

                    var resultMessage = new ServerMessage();
                    try
                    {
                        resultMessage.Id = Convert.ToUInt32(optionsDoc.SelectSingleNode(@"\options\id").InnerText);
                        resultMessage.MessageType = (Enums.MessageType)Convert.ToInt32(optionsDoc.SelectSingleNode(@"\options\messageType").InnerText);
                        var commandInnerText = optionsDoc.SelectSingleNode(@"\options\command").InnerText;
                        if (!string.IsNullOrEmpty(commandInnerText)) resultMessage.CommandType = (Enums.CommandType)Convert.ToUInt32(commandInnerText);
                        
                    }
                    catch(Exception ex)
                    {
                        // Corrupt document -- ignore
                        // TODO Log Error
                    }


                    // TODO Store IServerMessage in List

                    // TODO If message then notify subscribers
                    foreach (var subscriber in _subscribers)
                    {
                        subscriber.OnServerMessageReceived();
                    }
                }
            }
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


        public bool SendMessage(IServerMessage message)
        {
            if (_connectionActive && _client.Connected && message.SerializedMessage != null)
            {
                try
                {
                    // Dirty typecast (OH NOEZ AGAIN)
                    _client.GetStream().Write(message.SerializedMessage, 0, 8 + (int)message.OptionsSize + (int)message.ContentSize);
                }
                catch (Exception ex)
                {
                    // TODO Log Error
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
