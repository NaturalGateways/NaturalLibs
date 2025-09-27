using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Natural.Aws.DynamoDB
{
    public interface IDynamoTable
    {
        /// <summary>Getter for an item by its known key.</summary>
        Task<IDynamoItem> GetItemByKeyAsync(string partitionKey, string sortKey, string selectStatement);

        /// <summary>Getter for all items in a partition.</summary>
        Task<IEnumerable<IDynamoItem>> GetItemsAsync(string partitionKey, string sortKeyPrefix, string selectStatement);

        /// <summary>Getter for an item's value without creating an item object.</summary>
        Task<ObjectType> GetItemValueAsNullableObjectAsync<ObjectType>(string partitionKey, string sortKey, string columnName);

        /// <summary>Puts an item into the table.</summary>
        Task PutItemAsync(string partitionKey, string sortKey, ItemUpdate itemUpdate);
    }
}
