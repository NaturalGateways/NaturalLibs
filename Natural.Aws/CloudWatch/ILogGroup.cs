using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.CloudWatch
{
    public interface ILogGroup
    {
        /// <summary>Creates a stream to log to. Throws an exception if it already exists. Returns the new stream.</summary>
        Task<ILogStream> CreateStreamAsync(string streamName);

        /// <summary>Getter for a stream to log to. Returns an object even if stream doesn't exist.</summary>
        ILogStream GetStream(string streamName);

        /// <summary>Getter for multiple streams to log to.</summary>
        ILogStream GetTwoStreams(string streamName1, string streamName2);
    }
}
