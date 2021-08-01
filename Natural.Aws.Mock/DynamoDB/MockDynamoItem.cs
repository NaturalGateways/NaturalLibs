using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws.DynamoDB
{
    public class MockDynamoItem : IDynamoItem
    {
        #region Base

        /// <summary>String values by their key.</summary>
        public Dictionary<string, string> StringValuesByKey { get; set; }

        /// <summary>Constructor.</summary>
        public MockDynamoItem(Dictionary<string, string> stringValuesByKey)
        {
            this.StringValuesByKey = stringValuesByKey;
        }

        #endregion

        #region IDynamoItem implementation

        /// <summary>Getter for a string value.</summary>
        public string GetString(string key)
        {
            if (this.StringValuesByKey?.ContainsKey(key) ?? false)
            {
                return this.StringValuesByKey[key];
            }
            throw new NaturalException($"Cannot find item value with key '{key}'");
        }

        /// <summary>Getter for a string value as a JSON object.</summary>
        public JsonType GetStringAsObject<JsonType>(string key)
        {
            if (this.StringValuesByKey?.ContainsKey(key) ?? false)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<JsonType>(this.StringValuesByKey[key]);
            }
            throw new NaturalException($"Cannot find item value with key '{key}'.");
        }

        #endregion
    }
}
