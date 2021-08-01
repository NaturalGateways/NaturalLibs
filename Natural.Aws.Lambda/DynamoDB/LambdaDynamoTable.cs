using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Natural.Aws.DynamoDB
{
    public class LambdaDynamoTable : IDynamoTable
    {
        #region Base

        /// <summary>The DB client.</summary>
        private Amazon.DynamoDBv2.AmazonDynamoDBClient m_dbClient = null;

        /// <summary>The table name.</summary>
        private string m_tableName = null;

        /// <summary>The name of the partition key.</summary>
        private string m_partitionKeyName = null;
        /// <summary>The name of the sort key.</summary>
        private string m_sortKeyName = null;

        /// <summary>Constructor.</summary>
        public LambdaDynamoTable(Amazon.DynamoDBv2.AmazonDynamoDBClient dbClient, string tableName, string partitionKeyName, string sortKeyName)
        {
            m_dbClient = dbClient;
            m_tableName = tableName;
            m_partitionKeyName = partitionKeyName;
            m_sortKeyName = sortKeyName;
        }

        #endregion

        #region IDynamoTable implementation

        /// <summary>Getter for an item by its known key.</summary>
        public async Task<IDynamoItem> GetItemByKey(string partitionKey, string sortKey)
        {
            // Try cast to report the error properly
            try
            {
                // Create the request
                Amazon.DynamoDBv2.Model.GetItemRequest request = new Amazon.DynamoDBv2.Model.GetItemRequest
                {
                    TableName = m_tableName,
                    ProjectionExpression = "JsonData",
                    Key = new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                    {
                        { m_partitionKeyName, new Amazon.DynamoDBv2.Model.AttributeValue { S = partitionKey } },
                        { m_sortKeyName, new Amazon.DynamoDBv2.Model.AttributeValue { S = sortKey } }
                    }
                };

                // Get the item
                Amazon.DynamoDBv2.Model.GetItemResponse response = await m_dbClient.GetItemAsync(request);
                
                // Return
                return new LambdaDynamoItem(response.Item);
            }
            catch (Exception ex)
            {
                throw new NaturalException($"Cannot get item from table '{m_tableName}' where ({m_partitionKeyName}='{partitionKey}', {m_sortKeyName}='{sortKey}').", ex);
            }
        }

        #endregion
    }
}
