using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.CloudWatch
{
    public interface ILogStream
    {
        /// <summary>Logs a message.</summary>
        Task LogMessageAsync(string message);

        /// <summary>Logs an object as a JSON string as the message.</summary>
        Task LogObjectAsync(object logObject);
    }
}
