using System;
using System.Collections.Generic;
using System.Text;

namespace Natural.Aws.DynamoDB
{
    public interface IDynamoItem
    {
        /// <summary>Getter for a string value.</summary>
        string GetString(string key);

        /// <summary>Getter for a string value as a JSON object.</summary>
        JsonType GetStringAsObject<JsonType>(string key);
    }
}
