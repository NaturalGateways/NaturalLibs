using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws
{
    /// <summary>Implementation of an AWS service invoked from a Lambda.</summary>
    public class LambdaAwsService : IAwsService
    {
        #region Base

        /// <summary>The access credentials.</summary>
        private AwsAccessCredentials m_accessCredentials = null;

        /// <summary>The Dynamo service.</summary>
        private DynamoDB.IDynamoService m_dynamoService = null;
        /// <summary>The S3 service.</summary>
        private S3.IS3Service m_s3Service = null;

        /// <summary>Constructor.</summary>
        public LambdaAwsService()
        {
            //
        }

        /// <summary>Constructor.</summary>
        public LambdaAwsService(AwsAccessCredentials accessCredentials)
        {
            m_accessCredentials = accessCredentials;
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
                    m_dynamoService?.Dispose();
                    m_dynamoService = null;
                    m_s3Service?.Dispose();
                    m_s3Service = null;
                }
                m_disposed = true;
            }
        }

        #endregion

        #region IAwsService implementation

        /// <summar>Getter for the DynamoDB service.</summary>
        public DynamoDB.IDynamoService DynamoService
        {
            get
            {
                if (m_dynamoService == null)
                {
                    if (m_accessCredentials != null)
                        m_dynamoService = new DynamoDB.LambdaDynamoService(m_accessCredentials);
                    else
                        m_dynamoService = new DynamoDB.LambdaDynamoService();
                }
                return m_dynamoService;
            }
        }

        /// <summar>Getter for the S3 service.</summary>
        public S3.IS3Service S3Service
        {
            get
            {
                if (m_s3Service == null)
                {
                    if (m_accessCredentials != null)
                        m_s3Service = new S3.LambdaS3Service(m_accessCredentials);
                    else
                        m_s3Service = new S3.LambdaS3Service();
                }
                return m_s3Service;
            }
        }

        #endregion
    }
}
