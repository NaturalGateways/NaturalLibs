using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws.DynamoDB
{
    public class MockDynamoService : IDynamoService
    {
        #region Base

        /// <summary>The shared data across instances.</summary>
        private MockDynamoData m_data = null;

        /// <summary>Constructor.</summary>
        public MockDynamoService(MockDynamoData data)
        {
            m_data = data;
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            //
        }

        #endregion

        #region IDynamoService implementation

        /// <summary>Getter for a table with a given name.</summary>
        public IDynamoTable GetTable(string tableName, string partitionKeyName, string sortKeyName)
        {
            return m_data.TablesByName[tableName];
        }

        #endregion
    }
}
