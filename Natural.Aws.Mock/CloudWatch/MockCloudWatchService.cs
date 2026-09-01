using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.CloudWatch
{
    public class MockCloudWatchService : ICloudWatchService
    {
        #region ICloudWatchService implementation

        /// <summary>Getter for a log group. Returns an object whether or not the group exists.</summary>
        public ILogGroup GetLogGroup(string logGroupName)
        {
            return new MockCloudWatchLogGroup(logGroupName);
        }

        #endregion
    }
}
