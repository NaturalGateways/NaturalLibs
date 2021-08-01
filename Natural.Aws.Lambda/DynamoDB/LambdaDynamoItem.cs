using System;
using System.Collections.Generic;
using System.Linq;

namespace Natural.Aws.DynamoDB
{
    public class LambdaDynamoItem : IDynamoItem
    {
        #region Base

        /// <summary>The attributes.</summary>
        private Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue> m_attributes = null;

        /// <summary>Constructor.</summary>
        public LambdaDynamoItem(Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue> attributes)
        {
            m_attributes = attributes;
        }

        #endregion

        #region IDynamoItem implementation

        /// <summary>Getter for a string value.</summary>
        public string GetString(string key)
        {
            if (m_attributes.ContainsKey(key))
            {
                return m_attributes[key].S;
            }
            throw new NaturalException($"Cannot find item value with key '{key}': value keys are [{string.Join(", ", m_attributes.Select(x => x.Key))}]");
        }

        /// <summary>Getter for a string value as a JSON object.</summary>
        public JsonType GetStringAsObject<JsonType>(string key)
        {
            if (m_attributes.ContainsKey(key))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<JsonType>(m_attributes[key].S);
            }
            throw new NaturalException($"Cannot find item value with key '{key}': value keys are [{string.Join(", ", m_attributes.Select(x => x.Key))}]");
        }

        #endregion
    }
}
