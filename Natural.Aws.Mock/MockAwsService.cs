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

        /// <summary>Creates a table.</summary>
        public DynamoDB.MockDynamoTable CreateTable(string tableName)
        {
            DynamoDB.MockDynamoTable newTable = new DynamoDB.MockDynamoTable();
            m_data.TablesByName.Add(tableName, newTable);
            return newTable;
        }

        #endregion

        #region IAwsService implementation

        /// <summar>Creates a disposable DynamoDB service.</summary>
        public DynamoDB.IDynamoService CreateDynamoService()
        {
            return new DynamoDB.MockDynamoService(m_data);
        }

        #endregion
    }
}
