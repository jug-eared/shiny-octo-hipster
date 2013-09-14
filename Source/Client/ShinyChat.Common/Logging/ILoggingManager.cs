using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShinyChat.Common.Logging
{
    public interface ILoggingManager
    {
        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="error">Error to be logged</param>
        /// <param name="ex">Possible exception to add to the error message</param>
        void LogError(string error, Exception ex = null);

        /// <summary>
        /// Logs a warning
        /// </summary>
        /// <param name="warning">Warning to be logged</param>
        void LogWarning(string warning);

        /// <summary>
        /// Logs an information
        /// </summary>
        /// <param name="info">Information to be logged</param>
        void LogInformation(string info);
    }
}
