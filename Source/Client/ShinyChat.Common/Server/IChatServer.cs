using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ShinyChat.Common
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
        /// Method that gets executed when a message was received
        /// </summary>
        AsyncCallback MessageReceived { get; set; }

        /// <summary>
        /// Triggers start of the listener
        /// </summary>
        /// <returns>Boolean value indicating wether listening could be started without errors</returns>
        bool StartListen();

        /// <summary>
        /// Triggers end of the listener
        /// </summary>
        /// <returns>Boolean value indicating wether listening could be stopped without errors</returns>
        bool StopListen();

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
