using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws
{
    /// <summary>Implementation of an AWS service that is only in-memory.</summary>
    public class MockAwsService : IAwsService
    {
        #region Base

        /// <summary>The shared data across instances.</summary>
        private DynamoDB.MockDynamoData m_data = new DynamoDB.MockDynamoData();

        /// <summary>Constructor.</summary>
        public MockAwsService()
        {
            this.DynamoService = new DynamoDB.MockDynamoService(m_data);
        }

        /// <summary>Creates a table.</summary>
        public DynamoDB.MockDynamoTable CreateTable(string tableName)
        {
            DynamoDB.MockDynamoTable newTable = new DynamoDB.MockDynamoTable();
            m_data.TablesByName.Add(tableName, newTable);
            return newTable;
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            //
        }

        #endregion

        #region IAwsService implementation

        /// <summar>Getter for the DynamoDB service.</summary>
        public DynamoDB.IDynamoService DynamoService { get; private set; }

        /// <summar>Getter for the S3 service.</summary>
        public S3.IS3Service S3Service { get { return null; } }

        #endregion
    }
}
