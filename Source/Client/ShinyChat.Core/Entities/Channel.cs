using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common.Entities;

namespace ShinyChat.Core.Entities
{
    public class Channel : IChannel
    {
        private IMessageLog _messageLog;

        public string Name
        {
            get;
            set;
        }

        public IMessageLog MessageLog
        {
            get
            {
                if (IsOpened)
                {
                    if (_messageLog == null)
                        _messageLog = new MessageLog();

                    return _messageLog;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _messageLog = value;
            }
        }

        public bool IsOpened
        {
            get;
            set;
        }
    }
}
