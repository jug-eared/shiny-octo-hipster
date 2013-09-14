using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common.Entities;

namespace ShinyChat.Core.Entities
{
    public class InternalIdCounter : IInternalIdCounter
    {
        private int _id = 0;

        public int Id
        {
            get { return ++_id; }
        }
    }
}
