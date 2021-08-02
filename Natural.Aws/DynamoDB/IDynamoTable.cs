using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Natural.Aws.DynamoDB
{
    public interface IDynamoTable
    {
        /// <summary>Getter for an item by its known key.</summary>
        Task<IDynamoItem> GetItemByKeyAsync(string partitionKey, string sortKey, string selectStatement);

        /// <summary>Puts an item into the table.</summary>
        Task PutItemAsync(string partitionKey, string sortKey, ItemUpdate itemUpdate);
    }
}
