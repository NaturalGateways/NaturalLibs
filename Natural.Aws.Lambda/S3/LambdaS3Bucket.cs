using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.S3
{
    internal class LambdaS3Bucket : IS3Bucket
    {
        #region Base

        /// <summary>The S3 service.</summary>
        private IS3Service m_s3Service = null;

        /// <summary>The bucket name.</summary>
        private string m_bucketName = null;

        /// <summary>Constructor.</summary>
        public LambdaS3Bucket(IS3Service s3Service, string bucketName)
        {
            m_s3Service = s3Service;
            m_bucketName = bucketName;
        }

        #endregion

        #region IS3Bucket implementation

        /// <summary>Getter for a list of keys for objects under a prefix.</summary>
        public async Task<string[]> ListObjectsAsync(string keyPrefix)
        {
            return await m_s3Service.ListObjectsAsync(m_bucketName, keyPrefix);
        }

        /// <summary>Getter for an unsigned URL of an S3 object.</summary>
        public string GetObjectUrl(string key)
        {
            return m_s3Service.GetObjectUrl(m_bucketName, key);
        }

        /// <summary>Getter for the data of an S3 object, writing it to a stream.</summary>
        public async Task ReadObjectToStreamAsync(string key, Stream stream)
        {
            await m_s3Service.ReadObjectToStreamAsync(m_bucketName, key, stream);
        }

        /// <summary>Getter for a presigned URL for the given object.</summary>
        public async Task<string> GetPresignedObjectUrlAsync(string key, TimeSpan? duration = null)
        {
            return await m_s3Service.GetPresignedObjectUrlAsync(m_bucketName, key, duration);
        }

        /// <summary>Uploads an object to S3.</summary>
        public async Task UploadObjectFilepathAsync(string sourceFilepath, string destKey)
        {
            await m_s3Service.UploadObjectFilepathAsync(sourceFilepath, m_bucketName, destKey);
        }

        /// <summary>Uploads an object to S3.</summary>
        public async Task UploadObjectStreamAsync(Stream sourceStream, string destKey)
        {
            await m_s3Service.UploadObjectStreamAsync(sourceStream, m_bucketName, destKey);
        }

        /// <summary>Uploads an object to S3.</summary>
        public async Task UploadObjectJsonStringAsync(string sourceJsonString, string destKey)
        {
            await m_s3Service.UploadObjectJsonStringAsync(sourceJsonString, m_bucketName, destKey);
        }

        /// <summary>Deletes an object.</summary>
        public async Task DeleteObjectAsync(string key)
        {
            await m_s3Service.DeleteObjectAsync(m_bucketName, key);
        }

        #endregion
    }
}
