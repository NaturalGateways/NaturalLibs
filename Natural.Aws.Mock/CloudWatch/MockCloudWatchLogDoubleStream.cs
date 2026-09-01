using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.CloudWatch
{
    internal class MockCloudWatchLogDoubleStream : ILogStream
    {
        #region Base

        /// <summary>The log group name.</summary>
        private string m_logGroupName = null;
        /// <summary>The log stream name.</summary>
        private string m_logStreamName1 = null;
        /// <summary>The log stream name.</summary>
        private string m_logStreamName2 = null;

        /// <summary>Constructor.</summary>
        public MockCloudWatchLogDoubleStream(string logGroupName, string logStreamName1, string logStreamName2)
        {
            m_logGroupName = logGroupName;
            m_logStreamName1 = logStreamName1;
            m_logStreamName2 = logStreamName2;
        }

        #endregion

        #region ILogStream implementation

        /// <summary>Logs a message.</summary>
        public Task LogMessageAsync(string message)
        {
            Console.WriteLine($"{m_logGroupName} - {m_logStreamName1}: {message}");
            Console.WriteLine($"{m_logGroupName} - {m_logStreamName2}: {message}");
            return Task.CompletedTask;
        }

        /// <summary>Logs an object as a JSON string as the message.</summary>
        public Task LogObjectAsync(object logObject)
        {
            string messageJson = Json.JsonHelper.SerialiseObject(logObject);
            Console.WriteLine($"{m_logGroupName} - {m_logStreamName1}: {messageJson}");
            Console.WriteLine($"{m_logGroupName} - {m_logStreamName2}: {messageJson}");
            return Task.CompletedTask;
        }

        #endregion
    }
}
