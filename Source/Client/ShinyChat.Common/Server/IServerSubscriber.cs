using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Server
{
    public interface IServerSubscriber
    {
        /// <summary>
        /// Gets called when the server receives a message
        /// </summary>
        void OnServerMessageReceived();
    }
}
