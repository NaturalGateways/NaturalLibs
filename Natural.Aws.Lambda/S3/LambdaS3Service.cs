using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Aws.S3
{
    internal class LambdaS3Service : IS3Service
    {
        #region Base

        /// <summary>The S3 client.</summary>
        private Amazon.S3.IAmazonS3 m_s3Client = null;

        /// <summary>Constructor.</summary>
        public LambdaS3Service()
        {
            m_s3Client = new Amazon.S3.AmazonS3Client();
        }

        /// <summary>Constructor.</summary>
        public LambdaS3Service(AwsAccessCredentials accessCredentials)
        {
            m_s3Client = new Amazon.S3.AmazonS3Client(accessCredentials.AccessKeyId, accessCredentials.AccessKeySecret, accessCredentials.Region);
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
                    m_s3Client?.Dispose();
                    m_s3Client = null;
                }
                m_disposed = true;
            }
        }

        #endregion

        #region IS3Service implementation

        /// <summary>Getter for an object to interact with just a bucket.</summary>
        public IS3Bucket GetBucket(string bucketName)
        {
            return new LambdaS3Bucket(this, bucketName);
        }

        /// <summary>Getter for a list of keys for objects under a prefix.</summary>
        public async Task<string[]> ListObjectsAsync(string bucketName, string keyPrefix)
        {
            Amazon.S3.Model.ListObjectsV2Request request = new Amazon.S3.Model.ListObjectsV2Request
            {
                BucketName = bucketName
            };
            if (string.IsNullOrEmpty(keyPrefix) == false)
            {
                request.Prefix = keyPrefix;
            }
            Amazon.S3.Model.ListObjectsV2Response response = await m_s3Client.ListObjectsV2Async(request);
            return response.S3Objects.Select(x => x.Key).ToArray();
        }

        /// <summary>Getter for an unsigned URL of an S3 object.</summary>
        public string GetObjectUrl(string bucketName, string key)
        {
            string regionName = m_s3Client.Config.RegionEndpoint.SystemName;
            return $"https://{bucketName}.s3.{regionName}.amazonaws.com/{key}";
        }

        /// <summary>Getter for the data of an S3 object, writing it to a stream.</summary>
        public async Task ReadObjectToStreamAsync(string bucketName, string key, Stream stream)
        {
            Amazon.S3.Model.GetObjectResponse response = await m_s3Client.GetObjectAsync(bucketName, key);
            await response.ResponseStream.CopyToAsync(stream);
        }

        /// <summary>Getter for a presigned URL for the given object.</summary>
        public async Task<string> GetPresignedObjectUrlAsync(string bucketName, string key, TimeSpan? duration = null)
        {
            // Default timespan
            if (duration.HasValue == false)
                duration = new TimeSpan(0, 15, 0);
            // Do request
            Amazon.S3.Model.GetPreSignedUrlRequest request = new Amazon.S3.Model.GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = key,
                Expires = DateTime.UtcNow.Add(duration.Value)
            };
            return await m_s3Client.GetPreSignedURLAsync(request);
        }

        /// <summary>Uploads an object to S3.</summary>
        public async Task UploadObjectFilepathAsync(string sourceFilepath, string destBucketName, string destKey)
        {
            Amazon.S3.Model.PutObjectRequest request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = destBucketName,
                Key = destKey,
                FilePath = sourceFilepath
            };
            await m_s3Client.PutObjectAsync(request);
        }

        /// <summary>Uploads an object to S3.</summary>
        public async Task UploadObjectStreamAsync(Stream sourceStream, string destBucketName, string destKey)
        {
            // Amazon needs to seek, so cread to memory buffer if it can't seek
            if (sourceStream.CanSeek == false)
            {
                MemoryStream memoryStream = new MemoryStream();
                await sourceStream.CopyToAsync(memoryStream);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                sourceStream = memoryStream;
            }

            // Upload
            Amazon.S3.Model.PutObjectRequest request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = destBucketName,
                Key = destKey,
                InputStream = sourceStream
            };
            await m_s3Client.PutObjectAsync(request);
        }

        /// <summary>Uploads an object to S3.</summary>
        public async Task UploadObjectJsonStringAsync(string sourceJsonString, string destBucketName, string destKey)
        {
            Amazon.S3.Model.PutObjectRequest request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = destBucketName,
                Key = destKey,
                ContentType = "application/json",
                ContentBody = sourceJsonString
            };
            await m_s3Client.PutObjectAsync(request);
        }

        /// <summary>Deletes an object.</summary>
        public async Task DeleteObjectAsync(string bucketName, string key)
        {
            await m_s3Client.DeleteObjectAsync(bucketName, key);
        }

        #endregion
    }
}
