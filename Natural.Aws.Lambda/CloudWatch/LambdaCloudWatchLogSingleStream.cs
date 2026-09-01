using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Amazon.CloudWatchLogs.Model;

namespace Natural.Aws.CloudWatch
{
    internal class LambdaCloudWatchLogSingleStream : ILogStream
    {
        #region Base

        /// <summary>The CloudWatch client.</summary>
        private Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient m_cwClient = null;
        /// <summary>The log group name.</summary>
        private string m_logGroupName = null;
        /// <summary>The log stream name.</summary>
        private string m_logStreamName = null;

        /// <summary>Constructor.</summary>
        public LambdaCloudWatchLogSingleStream(Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient cwClient, string logGroupName, string logStreamName)
        {
            m_cwClient = cwClient;
            m_logGroupName = logGroupName;
            m_logStreamName = logStreamName;
        }

        #endregion

        #region ILogStream implementation

        /// <summary>Logs a message.</summary>
        public async Task LogMessageAsync(string message)
        {
            await m_cwClient.PutLogEventsAsync(new PutLogEventsRequest
            {
                LogGroupName = m_logGroupName,
                LogStreamName = m_logStreamName,
                LogEvents = new List<InputLogEvent>()
                    {
                        new InputLogEvent()
                        {
                            Message = message,
                            Timestamp = DateTime.UtcNow
                        }
                    }
            });
        }

        /// <summary>Logs an object as a JSON string as the message.</summary>
        public async Task LogObjectAsync(object logObject)
        {
            string messageJson = Json.JsonHelper.SerialiseObject(logObject);
            await m_cwClient.PutLogEventsAsync(new PutLogEventsRequest
            {
                LogGroupName = m_logGroupName,
                LogStreamName = m_logStreamName,
                LogEvents = new List<InputLogEvent>()
                    {
                        new InputLogEvent()
                        {
                            Message = messageJson,
                            Timestamp = DateTime.UtcNow
                        }
                    }
            });
        }

        #endregion
    }
}
