using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Entities
{
    public interface IInternalIdCounter
    {
        /// <summary>
        /// Gets an automatically increasing internal id
        /// </summary>
        int Id { get; }
    }
}
