using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

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
        public async Task<IDynamoItem> GetItemByKeyAsync(string partitionKey, string sortKey, string selectStatement)
        {
            // Try cast to report the error properly
            try
            {
                // Create the request
                Amazon.DynamoDBv2.Model.GetItemRequest request = new Amazon.DynamoDBv2.Model.GetItemRequest
                {
                    TableName = m_tableName,
                    ProjectionExpression = selectStatement,
                    Key = new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                    {
                        { m_partitionKeyName, new Amazon.DynamoDBv2.Model.AttributeValue { S = partitionKey } },
                        { m_sortKeyName, new Amazon.DynamoDBv2.Model.AttributeValue { S = sortKey } }
                    }
                };

                // Get the item
                Amazon.DynamoDBv2.Model.GetItemResponse response = await m_dbClient.GetItemAsync(request);
                
                // Return
                if (response.IsItemSet)
                {
                    return new LambdaDynamoItem(response.Item);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new NaturalException($"Cannot get item from table '{m_tableName}' where ({m_partitionKeyName}='{partitionKey}', {m_sortKeyName}='{sortKey}').", ex);
            }
        }

        /// <summary>Getter for all items in a partition.</summary>
        public async Task<IEnumerable<IDynamoItem>> GetItemsAsync(string partitionKey, string sortKeyPrefix, string selectStatement)
        {
            // Try cast to report the error properly
            try
            {
                // Create key condition
                Dictionary<string, Amazon.DynamoDBv2.Model.Condition> keyConditions = new Dictionary<string, Amazon.DynamoDBv2.Model.Condition>
                {
                    {
                        m_partitionKeyName,
                        new Amazon.DynamoDBv2.Model.Condition()
                        {
                            ComparisonOperator = Amazon.DynamoDBv2.ComparisonOperator.EQ,
                            AttributeValueList = new List<Amazon.DynamoDBv2.Model.AttributeValue>
                            {
                                new Amazon.DynamoDBv2.Model.AttributeValue { S = partitionKey }
                            }
                        }
                    }
                };
                if (string.IsNullOrEmpty(sortKeyPrefix) == false)
                {
                    keyConditions.Add(m_sortKeyName, new Amazon.DynamoDBv2.Model.Condition()
                    {
                        ComparisonOperator = Amazon.DynamoDBv2.ComparisonOperator.BEGINS_WITH,
                        AttributeValueList = new List<Amazon.DynamoDBv2.Model.AttributeValue>
                        {
                            new Amazon.DynamoDBv2.Model.AttributeValue { S = sortKeyPrefix }
                        }
                    });
                }

                // Create the request
                Amazon.DynamoDBv2.Model.QueryRequest request = new Amazon.DynamoDBv2.Model.QueryRequest
                {
                    TableName = m_tableName,
                    ProjectionExpression = selectStatement,
                    KeyConditions = keyConditions
                };

                // Get the items
                Amazon.DynamoDBv2.Model.QueryResponse response = m_dbClient.QueryAsync(request).Result;

                // Return
                return response.Items.Select(x => new LambdaDynamoItem(x));
            }
            catch (Exception ex)
            {
                throw new NaturalException($"Cannot get item from table '{m_tableName}' where ({m_partitionKeyName}='{partitionKey}', {m_sortKeyName} starts with '{sortKeyPrefix}').", ex);
            }
        }

        /// <summary>Getter for an item's value without creating an item object.</summary>
        public async Task<ObjectType> GetItemValueAsNullableObjectAsync<ObjectType>(string partitionKey, string sortKey, string columnName)
        {
            // Try cast to report the error properly
            try
            {
                // Create the request
                Amazon.DynamoDBv2.Model.GetItemRequest request = new Amazon.DynamoDBv2.Model.GetItemRequest
                {
                    TableName = m_tableName,
                    ProjectionExpression = columnName,
                    Key = new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
                    {
                        { m_partitionKeyName, new Amazon.DynamoDBv2.Model.AttributeValue { S = partitionKey } },
                        { m_sortKeyName, new Amazon.DynamoDBv2.Model.AttributeValue { S = sortKey } }
                    }
                };

                // Get the item
                Amazon.DynamoDBv2.Model.GetItemResponse response = await m_dbClient.GetItemAsync(request);

                // Return
                if (response.IsItemSet && response.Item.ContainsKey(columnName))
                {
                    string valueString = response.Item[columnName].S;
                    if (string.IsNullOrEmpty(valueString) == false)
                    {
                        return System.Text.Json.JsonSerializer.Deserialize<ObjectType>(valueString);
                    }
                }
                return default(ObjectType);
            }
            catch (Exception ex)
            {
                throw new NaturalException($"Cannot get item from table '{m_tableName}' where ({m_partitionKeyName}='{partitionKey}', {m_sortKeyName}='{sortKey}').", ex);
            }
        }

        /// <summary>Puts an item into the table.</summary>
        public async Task PutItemAsync(string partitionKey, string sortKey, ItemUpdate itemUpdate)
        {
            // Try cast to report the error properly
            try
            {
                // Insert into the database
                Amazon.DynamoDBv2.Model.PutItemRequest request = new Amazon.DynamoDBv2.Model.PutItemRequest
                {
                    TableName = m_tableName,
                    Item = new Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>()
                };
                request.Item.Add(m_partitionKeyName, new Amazon.DynamoDBv2.Model.AttributeValue { S = partitionKey });
                request.Item.Add(m_sortKeyName, new Amazon.DynamoDBv2.Model.AttributeValue { S = sortKey });
                if (itemUpdate.StringAttributes != null)
                {
                    foreach (KeyValuePair<string, string> stringAttribute in itemUpdate.StringAttributes)
                    {
                        request.Item.Add(stringAttribute.Key, new Amazon.DynamoDBv2.Model.AttributeValue { S = stringAttribute.Value });
                    }
                }
                if (itemUpdate.ObjectAttributes != null)
                {
                    foreach (KeyValuePair<string, object> objectAttribute in itemUpdate.ObjectAttributes)
                    {
                        request.Item.Add(objectAttribute.Key, new Amazon.DynamoDBv2.Model.AttributeValue { S = System.Text.Json.JsonSerializer.Serialize(objectAttribute.Value) });
                    }
                }
                await m_dbClient.PutItemAsync(request);
            }
            catch (Exception ex)
            {
                throw new NaturalException($"Cannot put item into table '{m_tableName}' where ({m_partitionKeyName}='{partitionKey}', {m_sortKeyName}='{sortKey}').", ex);
            }
        }

        #endregion
    }
}
