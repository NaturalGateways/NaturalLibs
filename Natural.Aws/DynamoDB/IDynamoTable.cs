using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Natural.Aws.DynamoDB
{
    public interface IDynamoTable
    {
        /// <summary>Getter for an item by its known key.</summary>
        Task<IDynamoItem> GetItemByKey(string partitionKey, string sortKey);
    }
}
