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
        public async Task<IDynamoItem> GetItemByKey(string partitionKey, string sortKey)
        {
            IDynamoItem item = m_itemListsBySortByPartition[partitionKey][sortKey].FirstOrDefault();
            return await Task.FromResult<IDynamoItem>(item);
        }

        #endregion
    }
}
