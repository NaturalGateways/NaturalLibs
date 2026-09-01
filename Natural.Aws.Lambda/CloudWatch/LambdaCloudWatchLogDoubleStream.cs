using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon.CloudWatchLogs.Model;

namespace Natural.Aws.CloudWatch
{
    internal class LambdaCloudWatchLogDoubleStream : ILogStream
    {
        #region Base

        /// <summary>The CloudWatch client.</summary>
        private Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient m_cwClient = null;
        /// <summary>The log group name.</summary>
        private string m_logGroupName = null;
        /// <summary>The log stream name.</summary>
        private string m_logStreamName1 = null;
        /// <summary>The log stream name.</summary>
        private string m_logStreamName2 = null;

        /// <summary>Constructor.</summary>
        public LambdaCloudWatchLogDoubleStream(Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient cwClient, string logGroupName, string logStreamName1, string logStreamName2)
        {
            m_cwClient = cwClient;
            m_logGroupName = logGroupName;
            m_logStreamName1 = logStreamName1;
            m_logStreamName2 = logStreamName2;
        }

        #endregion

        #region ILogStream implementation

        /// <summary>Logs a message.</summary>
        public async Task LogMessageAsync(string message)
        {
            List<InputLogEvent> logEvents = new List<InputLogEvent>()
            {
                new InputLogEvent()
                {
                    Message = message,
                    Timestamp = DateTime.UtcNow
                }
            };
            PutLogEventsRequest request1 = new PutLogEventsRequest
            {
                LogGroupName = m_logGroupName,
                LogStreamName = m_logStreamName1,
                LogEvents = logEvents
            };
            PutLogEventsRequest request2 = new PutLogEventsRequest
            {
                LogGroupName = m_logGroupName,
                LogStreamName = m_logStreamName2,
                LogEvents = logEvents
            };
            await Task.WhenAll<PutLogEventsResponse>(
                m_cwClient.PutLogEventsAsync(request1),
                m_cwClient.PutLogEventsAsync(request2)
            );
        }

        /// <summary>Logs an object as a JSON string as the message.</summary>
        public async Task LogObjectAsync(object logObject)
        {
            string messageJson = Json.JsonHelper.SerialiseObject(logObject);
            await LogMessageAsync(messageJson);
        }

        #endregion
    }
}
