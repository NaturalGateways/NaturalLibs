using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.S3
{
    public interface IS3Service : IDisposable
    {
        /// <summary>Getter for an unsigned URL of an S3 object.</summary>
        string GetObjectUrl(string bucketName, string key);

        /// <summary>Uploads an object to S3.</summary>
        Task UploadObjectAsync(string sourceFilepath, string destBucketName, string destKey);
    }
}
