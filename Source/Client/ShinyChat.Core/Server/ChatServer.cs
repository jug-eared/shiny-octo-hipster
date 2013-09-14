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
using ShinyChat.Core.Entities;
using ShinyChat.Core.DI;
using ShinyChat.Common.Logging;

namespace ShinyChat.Core.Server
{
    public class ChatServer : IChatServer
    {
        public ChatServer()
        {
            _client = new TcpClient();
            _subscribers = new List<IServerSubscriber>();
            KnownChannels = new List<IChannel>();
        }

        public TcpClient _client;
        private List<IServerSubscriber> _subscribers;
        private bool _connectionActive;

        public List<IChannel> KnownChannels
        {
            get;
            set;
        }

        public List<IServerMessage> IncomingMessages
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
                    var listenerThread = new Thread(new ThreadStart(SocketListener));
                    listenerThread.Start();
                }
                catch (Exception ex)
                {
                    DiContainer.Container.Resolve<ILoggingManager>().LogError("An error occurred while trying to connect to the server.", ex);
                    return false;
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
                    DiContainer.Container.Resolve<ILoggingManager>().LogError("An error occurred while trying to disconnect from the server.", ex);
                    return false;
                }
                return true;
            }
            return false;
        }

        private void SocketListener()
        {
            while (_connectionActive && _client.Connected)
            {
                if (_client.GetStream().DataAvailable)
                {
                    // Read Data from stream
                    var optionsSizeBuffer = new byte[4];
                    var contentSizeBuffer = new byte[4];

                    _client.GetStream().Read(optionsSizeBuffer, 0, 4);
                    _client.GetStream().Read(contentSizeBuffer, 0, 4);

                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(optionsSizeBuffer, 0, optionsSizeBuffer.Length);
                        Array.Reverse(contentSizeBuffer, 0, contentSizeBuffer.Length);
                    }

                    var optionsSize = (uint)BitConverter.ToInt32(optionsSizeBuffer, 0);
                    var contentSize = (uint)BitConverter.ToInt32(contentSizeBuffer, 0);

                    var optionsBuffer = new byte[optionsSize];
                    var contentBuffer = new byte[contentSize];

                    // Dirty typecast (OH NOEZ)
                    _client.GetStream().Read(optionsBuffer, 0, (int)optionsSize);
                    _client.GetStream().Read(contentBuffer, 0, (int)contentSize);

                    var optionsText = System.Text.Encoding.UTF8.GetString(optionsBuffer);
                    var contentText = System.Text.Encoding.UTF8.GetString(contentBuffer);

                    var optionsDoc = new XmlDocument();
                    optionsDoc.LoadXml(optionsText);
                    var contentDoc = new XmlDocument();
                    contentDoc.LoadXml(contentText);

                    var resultMessage = new ServerMessage();
                    try
                    {
                        resultMessage.Id = Convert.ToUInt32(optionsDoc.SelectSingleNode(@"/options/id").InnerText);
                        resultMessage.MessageType = (Enums.MessageType)Convert.ToInt32(optionsDoc.SelectSingleNode(@"/options/messageType").InnerText);
                        var commandInnerText = optionsDoc.SelectSingleNode(@"/options/command").InnerText;
                        if (!string.IsNullOrEmpty(commandInnerText)) resultMessage.CommandType = (Enums.CommandType)Convert.ToUInt32(commandInnerText);
                        var channelName = optionsDoc.SelectSingleNode(@"/options/channel").InnerText;
                        var existingChannels = KnownChannels.Where(p => p.Name == channelName);

                        if (existingChannels.Any())
                            resultMessage.Channel = existingChannels.First();
                        else
                        {
                            var channel = new Channel() { IsOpened = false, Name = channelName, MessageLog = new MessageLog() };
                            resultMessage.Channel = channel;
                            KnownChannels.Add(channel);
                        }

                        resultMessage.User = optionsDoc.SelectSingleNode(@"/options/user").InnerText;
                        resultMessage.MessageContent = contentDoc.SelectSingleNode(@"/message").InnerText;
                        resultMessage.OptionsSize = optionsSize;
                        resultMessage.ContentSize = contentSize;
                    }
                    catch(Exception ex)
                    {
                        // Corrupt document -- ignore
                        DiContainer.Container.Resolve<ILoggingManager>().LogError("An error occurred while trying to parse response document from server.", ex);
                    }

                    IncomingMessages.Add(resultMessage);

                    foreach (var subscriber in _subscribers)
                    {
                        subscriber.OnServerMessageReceived();
                    }
                }
                Thread.Sleep(100);
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
                    var sendingThread = new Thread(() => SendAsync(message));
                    sendingThread.Start();
                }
                catch (Exception ex)
                {
                    DiContainer.Container.Resolve<ILoggingManager>().LogError("An error occurred while trying to send a message to the server.", ex);
                    return false;
                }
                return true;
            }
            return false;
        }

        private void SendAsync(IServerMessage message)
        {
            // Dirty typecast (OH NOEZ AGAIN)
            _client.GetStream().Write(message.SerializedMessage, 0, 8 + (int)message.OptionsSize + (int)message.ContentSize);
            _client.GetStream().Flush();
        }
    }
}
