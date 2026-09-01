using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.CloudWatch
{
    public interface ICloudWatchService
    {
        /// <summary>Getter for a log group. Returns an object whether or not the group exists.</summary>
        ILogGroup GetLogGroup(string logGroupName);
    }
}
