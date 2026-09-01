using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Amazon.CloudWatchLogs.Model;

namespace Natural.Aws.CloudWatch
{
    internal class LambdaCloudWatchLogGroup : ILogGroup
    {
        #region Base

        /// <summary>The CloudWatch client.</summary>
        private Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient m_cwClient = null;
        /// <summary>The log group name.</summary>
        private string m_logGroupName = null;

        /// <summary>Constructor.</summary>
        public LambdaCloudWatchLogGroup(Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient cwClient, string logGroupName)
        {
            m_cwClient = cwClient;
            m_logGroupName = logGroupName;
        }

        #endregion

        #region ILogGroup implementation

        /// <summary>Creates a stream to log to. Throws an exception if it already exists. Returns the new stream.</summary>
        public async Task<ILogStream> CreateStreamAsync(string streamName)
        {
            // Create the log
            await m_cwClient.CreateLogStreamAsync(new CreateLogStreamRequest
            {
                LogGroupName = m_logGroupName,
                LogStreamName = streamName
            });

            // Return
            return new LambdaCloudWatchLogSingleStream(m_cwClient, m_logGroupName, streamName);
        }

        /// <summary>Getter for a stream to log to. Returns an object even if stream doesn't exist.</summary>
        public ILogStream GetStream(string streamName)
        {
            return new LambdaCloudWatchLogSingleStream(m_cwClient, m_logGroupName, streamName);
        }

        /// <summary>Getter for multiple streams to log to.</summary>
        public ILogStream GetTwoStreams(string streamName1, string streamName2)
        {
            return new LambdaCloudWatchLogDoubleStream(m_cwClient, m_logGroupName, streamName1, streamName2);
        }

        #endregion
    }
}
