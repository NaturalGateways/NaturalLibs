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

        #region IAwsService implementation

        /// <summar>Creates a disposable DynamoDB service.</summary>
        public DynamoDB.IDynamoService CreateDynamoService()
        {
            if (m_accessCredentials != null)
                return new DynamoDB.LambdaDynamoService(m_accessCredentials);
            return new DynamoDB.LambdaDynamoService();
        }

        #endregion
    }
}
