using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.CloudWatch
{
    internal class MockCloudWatchLogSingleStream : ILogStream
    {
        #region Base

        /// <summary>The log group name.</summary>
        private string m_logGroupName = null;
        /// <summary>The log stream name.</summary>
        private string m_logStreamName = null;

        /// <summary>Constructor.</summary>
        public MockCloudWatchLogSingleStream(string logGroupName, string logStreamName)
        {
            m_logGroupName = logGroupName;
            m_logStreamName = logStreamName;
        }

        #endregion

        #region ILogStream implementation

        /// <summary>Logs a message.</summary>
        public Task LogMessageAsync(string message)
        {
            Console.WriteLine($"{m_logGroupName} - {m_logStreamName}: {message}");
            return Task.CompletedTask;
        }

        /// <summary>Logs an object as a JSON string as the message.</summary>
        public Task LogObjectAsync(object logObject)
        {
            string messageJson = Json.JsonHelper.SerialiseObject(logObject);
            Console.WriteLine($"{m_logGroupName} - {m_logStreamName}: {messageJson}");
            return Task.CompletedTask;
        }

        #endregion
    }
}
