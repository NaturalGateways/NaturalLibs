using System.Reflection.Metadata.Ecma335;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Natural.Json
{
    public static class JsonHelper
    {
        #region Public facade

        /// <summary>Serialises the given object. Returning null when object is null.</summary>
        public static string SerialiseObject(object objectValue)
        {
            if (objectValue == null)
                return null;
            return JsonConvert.SerializeObject(objectValue);
        }

        /// <summary>Serialises the given object. Returning null when object is null.</summary>
        public static JsonType DeserialiseObject<JsonType>(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
                return default(JsonType);
            return JsonConvert.DeserializeObject<JsonType>(stringValue);
        }

        /// <summary>Creates a JSON interface for the given string of JSON</summary>
        public static IJsonObject JsonFromString(string stringValue)
        {
            object objectValue = JsonConvert.DeserializeObject<object>(stringValue);
            return JsonFromObject(objectValue);
        }

        /// <summary>Creates a JSON interface for the given object</summary>
        public static IJsonObject JsonFromObject(object objectValue)
        {
            if (objectValue == null)
            {
                return JsonNullObject.Null;
            }
            string objectTypeName = objectValue.GetType().FullName;
            switch (objectTypeName)
            {
                case "Newtonsoft.Json.Linq.JArray":
                    return new JsonArrayObject((JArray)objectValue);
                case "Newtonsoft.Json.Linq.JObject":
                    return new JsonDictionaryObject((JObject)objectValue);
                case "System.Boolean":
                    return new JsonBooleanObject((bool)objectValue);
                case "System.Double":
                    return new JsonDoubleObject((double)objectValue);
                case "System.Int32":
                    return new JsonLongObject((int)objectValue);
                case "System.Int64":
                    return new JsonLongObject((long)objectValue);
                case "System.Single":
                    return new JsonDoubleObject((float)objectValue);
                case "System.String":
                    return new JsonStringObject((string)objectValue);
                case "System.Text.Json.JsonElement":
                    return new SystemJsonObject((System.Text.Json.JsonElement)objectValue);
            }
            return JsonNullObject.Null;
        }

        #endregion
    }
}
