using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws.DynamoDB
{
    public class LambdaDynamoService : IDynamoService
    {
        #region Base

        /// <summary>The DB client.</summary>
        private Amazon.DynamoDBv2.AmazonDynamoDBClient m_dbClient = null;

        /// <summary>Constructor.</summary>
        public LambdaDynamoService()
        {
            m_dbClient = new Amazon.DynamoDBv2.AmazonDynamoDBClient();
        }

        /// <summary>Constructor.</summary>
        public LambdaDynamoService(AwsAccessCredentials accessCredentials)
        {
            m_dbClient = new Amazon.DynamoDBv2.AmazonDynamoDBClient(accessCredentials.AccessKeyId, accessCredentials.AccessKeySecret, accessCredentials.Region);
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
                    m_dbClient?.Dispose();
                    m_dbClient = null;
                }
                m_disposed = true;
            }
        }

        #endregion

        #region IDynamoService implementation

        /// <summary>Getter for a table with a given name.</summary>
        public IDynamoTable GetTable(string tableName, string partitionKeyName, string sortKeyName)
        {
            return new LambdaDynamoTable(m_dbClient, tableName, partitionKeyName, sortKeyName);
        }

        #endregion
    }
}
