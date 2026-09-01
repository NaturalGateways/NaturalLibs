using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.CloudWatch
{
    internal class MockCloudWatchLogGroup : ILogGroup
    {
        #region Base

        /// <summary>The log group name.</summary>
        private string m_logGroupName = null;

        /// <summary>Constructor.</summary>
        public MockCloudWatchLogGroup(string logGroupName)
        {
            m_logGroupName = logGroupName;
        }

        #endregion

        #region ILogGroup implementation

        /// <summary>Creates a stream to log to. Throws an exception if it already exists. Returns the new stream.</summary>
        public async Task<ILogStream> CreateStreamAsync(string streamName)
        {
            return new MockCloudWatchLogSingleStream(m_logGroupName, streamName);
        }

        /// <summary>Getter for a stream to log to. Returns an object even if stream doesn't exist.</summary>
        public ILogStream GetStream(string streamName)
        {
            return new MockCloudWatchLogSingleStream(m_logGroupName, streamName);
        }

        /// <summary>Getter for multiple streams to log to.</summary>
        public ILogStream GetTwoStreams(string streamName1, string streamName2)
        {
            return new MockCloudWatchLogDoubleStream(m_logGroupName, streamName1, streamName2);
        }

        #endregion
    }
}
