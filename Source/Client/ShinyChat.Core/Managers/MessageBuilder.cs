﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common.Managers;
using ShinyChat.Common.Entities;
using ShinyChat.Core.DI;

namespace ShinyChat.Core.Managers
{
    public class MessageBuilder : IMessageBuilder
    {
        public IServerMessage BuildCommand(Enums.CommandType commandType, IChannel channel, string user)
        {
            var resultCommand = new ServerMessage();
            resultCommand.Id = (uint)DiContainer.Container.Resolve<IInternalIdCounter>().Id;
            resultCommand.Channel = channel;
            resultCommand.CommandType = commandType;
            resultCommand.MessageType = Enums.MessageType.Command;
            resultCommand.User = user;

            var optionsHeader = "<options>";
            optionsHeader += "<id>" + resultCommand.Id + "</id>";
            optionsHeader += "<messageType>" + Convert.ToInt32(resultCommand.MessageType) + "</messageType>";
            optionsHeader += "<command>" + Convert.ToInt32(resultCommand.CommandType) + "</command>";
            optionsHeader += commandType == Enums.CommandType.GetChannels ? "<channel></channel>" : "<channel>" + resultCommand.Channel.Name + "</channel>";
            optionsHeader += "<user>" + user + "</user>";
            optionsHeader += "</options>";

            var commandMessage = "<message></message>";

            var optionsBytes = System.Text.Encoding.UTF8.GetBytes(optionsHeader);
            resultCommand.OptionsSize = (uint)optionsBytes.Count();

            var messageBytes = System.Text.Encoding.UTF8.GetBytes(commandMessage);
            resultCommand.ContentSize = (uint)messageBytes.Count();

            var optionsByteSize = BitConverter.GetBytes(resultCommand.OptionsSize);
            var messageByteSize = BitConverter.GetBytes(resultCommand.ContentSize);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(optionsByteSize, 0, optionsByteSize.Length);
                Array.Reverse(messageByteSize, 0, messageByteSize.Length);
            }
            
            resultCommand.SerializedMessage = ConcatBytes(ConcatBytes(optionsByteSize, messageByteSize), ConcatBytes(optionsBytes, messageBytes));

            return resultCommand;
        }

        public IServerMessage BuildServerMessage(IChannel channel, string user, string message)
        {
            var resultCommand = new ServerMessage();
            resultCommand.Id = (uint)DiContainer.Container.Resolve<IInternalIdCounter>().Id;
            resultCommand.Channel = channel;
            resultCommand.CommandType = Enums.CommandType.Undefined;
            resultCommand.MessageType = Enums.MessageType.Message;
            resultCommand.User = user;

            var optionsHeader = "<options>";
            optionsHeader += "<id>" + resultCommand.Id + "</id>";
            optionsHeader += "<messageType>" + Convert.ToInt32(resultCommand.MessageType) + "</messageType>";
            optionsHeader += "<command>" + Convert.ToInt32(resultCommand.CommandType) + "</command>";
            optionsHeader += "<channel>" + resultCommand.Channel.Name + "</channel>";
            optionsHeader += "<user>" + user + "</user>";
            optionsHeader += "</options>";

            var commandMessage = "<message>" + message + "</message>";

            var optionsBytes = System.Text.Encoding.UTF8.GetBytes(optionsHeader);
            resultCommand.OptionsSize = (uint)optionsBytes.Count();

            var messageBytes = System.Text.Encoding.UTF8.GetBytes(commandMessage);
            resultCommand.ContentSize = (uint)messageBytes.Count();

            var optionsByteSize = BitConverter.GetBytes(resultCommand.OptionsSize);
            var messageByteSize = BitConverter.GetBytes(resultCommand.ContentSize);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(optionsByteSize, 0, optionsByteSize.Length);
                Array.Reverse(messageByteSize, 0, messageByteSize.Length);
            }

            resultCommand.SerializedMessage = ConcatBytes(ConcatBytes(optionsByteSize, messageByteSize), ConcatBytes(optionsBytes, messageBytes));

            return resultCommand;
        }

        private byte[] ConcatBytes(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Buffer.BlockCopy(a, 0, c, 0, a.Length);
            System.Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        private string BytesToString(byte[] arr)
        {
            var result = string.Empty;
            foreach (var byt in arr)
            {
                result += byt.ToString("X2");
            }

            return result;
        }
    }
}
