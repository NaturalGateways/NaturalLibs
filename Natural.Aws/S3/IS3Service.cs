using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.S3
{
    public interface IS3Service : IDisposable
    {
        /// <summary>Getter for an object to interact with just a bucket.</summary>
        IS3Bucket GetBucket(string bucketName);

        /// <summary>Getter for a list of keys for objects under a prefix.</summary>
        Task<string[]> ListObjectsAsync(string bucketName, string keyPrefix);

        /// <summary>Getter for an unsigned URL of an S3 object.</summary>
        string GetObjectUrl(string bucketName, string key);

        /// <summary>Getter for the data of an S3 object, writing it to a stream.</summary>
        Task ReadObjectToStreamAsync(string bucketName, string key, Stream stream);

        /// <summary>Getter for a presigned URL for the given object.</summary>
        Task<string> GetPresignedObjectUrlAsync(string bucketName, string key, TimeSpan? duration = null);

        /// <summary>Uploads an object to S3.</summary>
        Task UploadObjectFilepathAsync(string sourceFilepath, string destBucketName, string destKey);

        /// <summary>Uploads an object to S3.</summary>
        Task UploadObjectStreamAsync(Stream sourceStream, string destBucketName, string destKey);

        /// <summary>Uploads an object to S3.</summary>
        Task UploadObjectJsonStringAsync(string sourceJsonString, string destBucketName, string destKey);

        /// <summary>Deletes an object.</summary>
        Task DeleteObjectAsync(string bucketName, string key);
    }
}
