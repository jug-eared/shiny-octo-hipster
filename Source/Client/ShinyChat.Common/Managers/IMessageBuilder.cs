using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common.Entities;

namespace ShinyChat.Common.Managers
{
    public interface IMessageBuilder
    {
        
        /// <summary>
        /// Builds a command message that can be send to the server
        /// </summary>
        /// <param name="commandType">Type of the command</param>
        /// <param name="channel">Channel parameter of the message</param>
        /// <param name="user">User the message was sent by</param>
        /// <returns>IServerMessage containing the byte array that can be send to the server</returns>
        IServerMessage BuildCommand(Enums.CommandType commandType, IChannel channel, string user);


        // TODO: param message
        /// <summary>
        /// Builds a message that can be send to the server
        /// </summary>
        /// <param name="channel">Channel parameter of the message</param>
        /// <param name="user">User the message was sent by</param>
        /// <returns>IServerMessage containing the byte array that can be send to the server</returns>
        IServerMessage BuildServerMessage(IChannel channel, string user,string message);
    }
}
