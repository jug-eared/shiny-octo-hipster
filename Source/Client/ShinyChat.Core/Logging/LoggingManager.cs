using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShinyChat.Common.Logging;
using System.IO;

namespace ShinyChat.Core.Logging
{
    public class LoggingManager : ILoggingManager
    {
        private string workingDir;
        private StreamWriter streamWriter;

        public LoggingManager()
        {
            workingDir = Path.Combine(Environment.CurrentDirectory, "logs") + "\\";
            if (!Directory.Exists(workingDir))
                Directory.CreateDirectory(workingDir);
            streamWriter = new StreamWriter(workingDir + "shinylog_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".log");
            streamWriter.AutoFlush = true;
        }

        public void LogError(string error, Exception ex = null)
        {
            var logMessage = string.Empty;

            if (ex != null)
            {
                logMessage = string.Format("{0} [Error] {1} - Exception: {2} - StackTrace: {3}",DateTime.Now.ToString(),error,ex.Message,ex.StackTrace);
            }
            else
            {
                logMessage = string.Format("{0} [Error] {1}", DateTime.Now.ToString(), error);
            }
            
            streamWriter.Write(logMessage);
        }

        public void LogWarning(string warning)
        {
            var logMessage = string.Format("{0} [Warning] {1}", DateTime.Now.ToString(), warning);
            streamWriter.Write(logMessage);
        }

        public void LogInformation(string info)
        {
            var logMessage = string.Format("{0} [Information] {1}", DateTime.Now.ToString(), info);
            streamWriter.Write(logMessage);
        }
    }
}
