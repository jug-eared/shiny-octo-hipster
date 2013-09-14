using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ShinyChat.Common.Entities;

namespace ShinyChat.Common.Server
{
    public interface IChatServer
    {
        /// <summary>
        /// Listener Port
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Listener IP (0.0.0.0)
        /// </summary>
        IPAddress Ip { get; set; }

        /// <summary>
        /// Listener Endpoint
        /// </summary>
        IPEndPoint Endpoint { get; set; }

        /// <summary>
        /// Enumerable containing all unprocessed messages coming from server
        /// </summary>
        IEnumerable<IServerMessage> IncomingMessages { get; set; }

        /// <summary>
        /// Sends a given IServerMessage to the current server
        /// </summary>
        /// <param name="message">IServerMessage that is going to be sent to the server</param>
        /// <returns>Boolean value indicating wether transmission succeeded without errors</returns>
        bool SendMessage(IServerMessage message);

        /// <summary>
        /// Subscribes an IServerSubscriber to the chat server
        /// </summary>
        /// <param name="subscriber">Subscriber to subscribe the server</param>
        void Subscribe(IServerSubscriber subscriber);

        /// <summary>
        /// Unubscribes an IServerSubscriber to the chat server
        /// </summary>
        /// <param name="subscriber">Subscriber to unsubscribe the server</param>
        void Unsubscribe(IServerSubscriber subscriber);

        /// <summary>
        /// Opens connection to the server
        /// </summary>
        /// <returns>Boolean value indicating if connection to the server succeeded without errors</returns>
        bool OpenConnection();

        /// <summary>
        /// Closes connection to the server
        /// </summary>
        /// <returns>Boolean value indicating if connection to the server could be closed without errors</returns>
        bool CloseConnection();
    }
}
