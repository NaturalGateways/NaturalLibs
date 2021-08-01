using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws.DynamoDB
{
    /// <summary>The interface for interacting with DynamoDB tables.</summary>
    public interface IDynamoService : IDisposable
    {
        /// <summary>Getter for a table with a given name.</summary>
        IDynamoTable GetTable(string tableName, string partitionKeyName, string sortKeyName);
    }
}
