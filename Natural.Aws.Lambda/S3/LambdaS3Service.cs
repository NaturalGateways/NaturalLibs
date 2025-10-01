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

        /// <summary>Getter for an unsigned URL of an S3 object.</summary>
        public string GetObjectUrl(string bucketName, string key)
        {
            string regionName = m_s3Client.Config.RegionEndpoint.SystemName;
            return $"https://{bucketName}.s3.{regionName}.amazonaws.com/{key}";
        }

        /// <summary>Uploads an object to S3.</summary>
        public async Task UploadObjectAsync(string sourceFilepath, string destBucketName, string destKey)
        {
            Amazon.S3.Model.PutObjectRequest request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = destBucketName,
                Key = destKey,
                FilePath = sourceFilepath
            };
            await m_s3Client.PutObjectAsync(request);
        }

        #endregion
    }
}
