using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Natural.Aws.DynamoDB
{
    public class MockDynamoTable : IDynamoTable
    {
        #region Base

        /// <summary>The items.</summary>
        private Dictionary<string, Dictionary<string, List<IDynamoItem>>> m_itemListsBySortByPartition = new Dictionary<string, Dictionary<string, List<IDynamoItem>>>();

        /// <summary>Constructor.</summary>
        public MockDynamoTable()
        {
            //
        }

        /// <summary>Adds an item to the table.</summary>
        public void AddItem(string partitionKey, string sortKey, Dictionary<string, string> stringAttributes)
        {
            if (m_itemListsBySortByPartition.ContainsKey(partitionKey))
            {
                Dictionary<string, List<IDynamoItem>> itemListsBySort = m_itemListsBySortByPartition[partitionKey];
                if (itemListsBySort.ContainsKey(sortKey))
                {
                    itemListsBySort[sortKey].Add(new MockDynamoItem(stringAttributes));
                }
                else
                {
                    itemListsBySort.Add(sortKey, new List<IDynamoItem> { new MockDynamoItem(stringAttributes) });
                }
            }
            else
            {
                m_itemListsBySortByPartition.Add(partitionKey, new Dictionary<string, List<IDynamoItem>>
                {
                    { sortKey, new List<IDynamoItem> { new MockDynamoItem(stringAttributes) } }
                });
            }    
        }

        #endregion

        #region IDynamoTable implementation

        /// <summary>Getter for an item by its known key.</summary>
        public Task<IDynamoItem> GetItemByKeyAsync(string partitionKey, string sortKey, string selectStatement)
        {
            if (m_itemListsBySortByPartition.ContainsKey(partitionKey) == false)
                return Task.FromResult<IDynamoItem>(null);
            Dictionary<string, List<IDynamoItem>> partition = m_itemListsBySortByPartition[partitionKey];
            if (partition.ContainsKey(sortKey) == false)
                return Task.FromResult<IDynamoItem>(null);
            IDynamoItem item = partition[sortKey].FirstOrDefault();
            return Task.FromResult<IDynamoItem>(item);
        }

        /// <summary>Getter for all items in a partition.</summary>
        public Task<IEnumerable<IDynamoItem>> GetItemsAsync(string partitionKey, string sortKeyPrefix, string selectStatement)
        {
            IEnumerable<KeyValuePair<string, List<IDynamoItem>>> baseItems = m_itemListsBySortByPartition[partitionKey].AsEnumerable();
            if (string.IsNullOrEmpty(sortKeyPrefix) == false)
            {
                baseItems = baseItems.Where(x => x.Key.StartsWith(sortKeyPrefix));
            }
            IDynamoItem[] items = baseItems.SelectMany(x => x.Value).ToArray();
            return Task.FromResult<IEnumerable<IDynamoItem>>(items);
        }

        /// <summary>Getter for an item's value without creating an item object.</summary>
        public Task<ObjectType> GetItemValueAsNullableObjectAsync<ObjectType>(string partitionKey, string sortKey, string columnName)
        {
            if (m_itemListsBySortByPartition.ContainsKey(partitionKey) == false)
                return Task.FromResult<ObjectType>(default(ObjectType));
            Dictionary<string, List<IDynamoItem>> partition = m_itemListsBySortByPartition[partitionKey];
            if (partition.ContainsKey(sortKey) == false)
                return Task.FromResult<ObjectType>(default(ObjectType));
            IDynamoItem item = partition[sortKey].FirstOrDefault();
            if (item == null)
                return Task.FromResult<ObjectType>(default(ObjectType));
            return Task.FromResult<ObjectType>(item.GetStringAsObject<ObjectType>(columnName));
        }

        /// <summary>Puts an item into the table.</summary>
        public Task PutItemAsync(string partitionKey, string sortKey, ItemUpdate itemUpdate)
        {
            Dictionary<string, string> stringAttributes = new Dictionary<string, string>();
            if (itemUpdate.StringAttributes != null)
            {
                foreach (KeyValuePair<string, string> stringAttribute in itemUpdate.StringAttributes)
                {
                    stringAttributes.Add(stringAttribute.Key, stringAttribute.Value);
                }
            }
            if (itemUpdate.ObjectAttributes != null)
            {
                foreach (KeyValuePair<string, object> objectAttribute in itemUpdate.ObjectAttributes)
                {
                    stringAttributes.Add(objectAttribute.Key, System.Text.Json.JsonSerializer.Serialize(objectAttribute.Value));
                }
            }
            AddItem(partitionKey, sortKey, stringAttributes);
            return Task.CompletedTask;
        }

        #endregion
    }
}
