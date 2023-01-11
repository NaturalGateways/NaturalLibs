using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Natural.Json
{
    internal class SystemJsonObject : IJsonObject
    {
        #region Base

        /// <summary>The JSON element.</summary>
        private JsonElement m_jsonElement;

        /// <summary>Constructor.</summary>
        public SystemJsonObject(JsonElement jsonElement)
        {
            m_jsonElement = jsonElement;
        }

        #endregion

        #region IJsonObject implementation

        /// <summary>Getter for the object type.</summary>
        public JsonObjectType ObjectType
        {
            get
            {
                switch (m_jsonElement.ValueKind)
                {
                    case JsonValueKind.Array:
                        return JsonObjectType.Array;
                    case JsonValueKind.False:
                    case JsonValueKind.True:
                        return JsonObjectType.Boolean;
                    case JsonValueKind.Object:
                        return JsonObjectType.Dictionary;
                    case JsonValueKind.String:
                        return JsonObjectType.String;
                    case JsonValueKind.Number:
                        return JsonObjectType.Double;
                    default:
                        return JsonObjectType.Null;
                }
            }
        }

        /// <summary>Getter for the object as a string.</summary>
        public string AsString
        {
            get
            {
                switch (m_jsonElement.ValueKind)
                {
                    case JsonValueKind.String:
                        return m_jsonElement.GetString()!;
                    case JsonValueKind.Number:
                        return m_jsonElement.GetDouble().ToString();
                    case JsonValueKind.False:
                        return "false";
                    case JsonValueKind.True:
                        return "true";
                    default:
                        return null;
                }
            }
        }
        /// <summary>Getter for the object as a long integer.</summary>
        public long? AsLong
        {
            get
            {
                if (m_jsonElement.ValueKind != JsonValueKind.Number)
                    return null;
                long value = 0;
                if (m_jsonElement.TryGetInt64(out value) == false)
                    return null;
                return value;
            }
        }
        /// <summary>Getter for the object as a double float.</summary>
        public double? AsDouble
        {
            get
            {
                if (m_jsonElement.ValueKind != JsonValueKind.Number)
                    return null;
                double value = 0;
                if (m_jsonElement.TryGetDouble(out value) == false)
                    return null;
                return value;
            }
        }
        /// <summary>Getter for the object as a boolean.</summary>
        public bool? AsBoolean
        {
            get
            {
                switch (m_jsonElement.ValueKind)
                {
                    case JsonValueKind.False:
                        return false;
                    case JsonValueKind.True:
                        return true;
                    default:
                        return null;
                }
            }
        }
        /// <summary>Getter for the object as an array of object.</summary>
        public IJsonObject[] AsObjectArray
        {
            get
            {
                if (m_jsonElement.ValueKind == JsonValueKind.Array)
                    return m_jsonElement.EnumerateArray().Select(x => new SystemJsonObject(x)).ToArray();
                return null;
            }
        }
        /// <summary>Getter for the object as a boolean.</summary>
        public Dictionary<string, IJsonObject> AsObjectDictionary
        {
            get
            {
                if (m_jsonElement.ValueKind == JsonValueKind.Object)
                    return m_jsonElement.EnumerateObject().ToDictionary(x => x.Name, y => (IJsonObject)(new SystemJsonObject(y.Value)));
                return null;
            }
        }

        /// <summary>Getter for a object at the given index.</summary>
        public IJsonObject GetArrayObject(int index)
        {
            if (m_jsonElement.GetArrayLength() <= index)
                return JsonNullObject.Null;
            JsonElement childElement = m_jsonElement.EnumerateArray().Skip(index).First();
            return new SystemJsonObject(childElement);
        }

        /// <summary>Getter for a string at the given index.</summary>
        public string GetArrayString(int index)
        {
            if (m_jsonElement.GetArrayLength() <= index)
                return null;
            JsonElement childElement = m_jsonElement.EnumerateArray().Skip(index).First();
            switch (childElement.ValueKind)
            {
                case JsonValueKind.String:
                    return childElement.GetString()!;
                case JsonValueKind.Number:
                    return childElement.GetDouble().ToString();
                case JsonValueKind.False:
                    return "false";
                case JsonValueKind.True:
                    return "true";
                default:
                    return null;
            }
        }

        /// <summary>Getter for a long integer at the given index.</summary>
        public long? GetArrayLong(int index)
        {
            if (m_jsonElement.GetArrayLength() <= index)
                return null;
            JsonElement childElement = m_jsonElement.EnumerateArray().Skip(index).First();
            if (childElement.ValueKind != JsonValueKind.Number)
                return null;
            long value = 0;
            if (childElement.TryGetInt64(out value) == false)
                return null;
            return value;
        }

        /// <summary>Getter for a long integer at the given index.</summary>
        public double? GetArrayDouble(int index)
        {
            if (m_jsonElement.GetArrayLength() <= index)
                return null;
            JsonElement childElement = m_jsonElement.EnumerateArray().Skip(index).First();
            if (childElement.ValueKind != JsonValueKind.Number)
                return null;
            double value = 0;
            if (childElement.TryGetDouble(out value) == false)
                return null;
            return value;
        }

        /// <summary>Getter for a boolean at the given index.</summary>
        public bool? GetArrayBoolean(int index)
        {
            if (m_jsonElement.GetArrayLength() <= index)
                return null;
            JsonElement childElement = m_jsonElement.EnumerateArray().Skip(index).First();
            switch (childElement.ValueKind)
            {
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.True:
                    return true;
                default:
                    return null;
            }
        }

        /// <summary>Getter for a object with the given key.</summary>
        public IJsonObject GetDictionaryObject(string key)
        {
            JsonElement childElement;
            if (m_jsonElement.TryGetProperty(key, out childElement) == false)
                return JsonNullObject.Null;
            return new SystemJsonObject(childElement);
        }

        /// <summary>Getter for a string with the given key.</summary>
        public string GetDictionaryString(string key)
        {
            JsonElement childElement;
            if (m_jsonElement.TryGetProperty(key, out childElement) == false)
                return null;
            switch (childElement.ValueKind)
            {
                case JsonValueKind.String:
                    return childElement.GetString()!;
                case JsonValueKind.Number:
                    return childElement.GetDouble().ToString();
                case JsonValueKind.False:
                    return "false";
                case JsonValueKind.True:
                    return "true";
                default:
                    return null;
            }
        }

        /// <summary>Getter for a long integer with the given key.</summary>
        public long? GetDictionaryLong(string key)
        {
            JsonElement childElement;
            if (m_jsonElement.TryGetProperty(key, out childElement) == false)
                return null;
            if (childElement.ValueKind != JsonValueKind.Number)
                return null;
            long value = 0;
            if (childElement.TryGetInt64(out value) == false)
                return null;
            return value;
        }

        /// <summary>Getter for a long integer with the given key.</summary>
        public double? GetDictionaryDouble(string key)
        {
            JsonElement childElement;
            if (m_jsonElement.TryGetProperty(key, out childElement) == false)
                return null;
            if (childElement.ValueKind != JsonValueKind.Number)
                return null;
            double value = 0;
            if (childElement.TryGetDouble(out value) == false)
                return null;
            return value;
        }

        /// <summary>Getter for a boolean with the given key.</summary>
        public bool? GetDictionaryBoolean(string key)
        {
            JsonElement childElement;
            if (m_jsonElement.TryGetProperty(key, out childElement) == false)
                return null;
            switch (childElement.ValueKind)
            {
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.True:
                    return true;
                default:
                    return null;
            }
        }

        #endregion
    }
}
