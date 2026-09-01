using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.CloudWatch
{
    public class LambdaCloudWatchService : ICloudWatchService, IDisposable
    {
        #region Base

        /// <summary>The CloudWatch client.</summary>
        private Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient m_cwClient = null;

        /// <summary>Constructor.</summary>
        public LambdaCloudWatchService()
        {
            m_cwClient = new Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient();
        }

        /// <summary>Constructor.</summary>
        public LambdaCloudWatchService(AwsAccessCredentials accessCredentials)
        {
            m_cwClient = new Amazon.CloudWatchLogs.AmazonCloudWatchLogsClient(accessCredentials.AccessKeyId, accessCredentials.AccessKeySecret, accessCredentials.Region);
        }

        #endregion

        #region IDisposable implementation

        private bool m_disposed = false; // to detect redundant calls

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (m_disposed == false)
            {
                if (disposing)
                {
                    m_cwClient?.Dispose();
                    m_cwClient = null;
                }
                m_disposed = true;
            }
        }

        #endregion

        #region ICloudWatchService implementation

        /// <summary>Getter for a log group. Returns an object whether or not the group exists.</summary>
        public ILogGroup GetLogGroup(string logGroupName)
        {
            return new LambdaCloudWatchLogGroup(m_cwClient, logGroupName);
        }

        #endregion
    }
}
