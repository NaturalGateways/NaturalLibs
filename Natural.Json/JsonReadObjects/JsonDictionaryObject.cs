using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Natural.Json
{
    internal class JsonDictionaryObject : IJsonObject
    {
        #region Base

        /// <summary>The JSON object.</summary>
        private JObject m_jObject = null;

        /// <summary>Constructor.</summary>
        public JsonDictionaryObject(JObject jObject)
        {
            m_jObject = jObject;
        }

        #endregion

        #region IJsonObject implementation

        /// <summary>Getter for the object type.</summary>
        public JsonObjectType ObjectType { get { return JsonObjectType.Dictionary; } }

        /// <summary>Getter for the object as a string.</summary>
        public string AsString { get { return null; } }
        /// <summary>Getter for the object as a long integer.</summary>
        public long? AsLong { get { return null; } }
        /// <summary>Getter for the object as a double float.</summary>
        public double? AsDouble { get { return null; } }
        /// <summary>Getter for the object as a boolean.</summary>
        public bool? AsBoolean { get { return null; } }
        /// <summary>Getter for the object as an array of object.</summary>
        public IJsonObject[] AsObjectArray { get { return m_jObject.Values().Select(x => JsonFactory.JsonFromToken(x)).ToArray(); } }
        /// <summary>Getter for the object as a boolean.</summary>
        public Dictionary<string, IJsonObject> AsObjectDictionary
        {
            get
            {
                Dictionary<string, IJsonObject> objectsByKey = new Dictionary<string, IJsonObject>();
                foreach (KeyValuePair<string, JToken?> keyValue in m_jObject)
                {
                    if (keyValue.Value != null)
                    {
                        IJsonObject valueJson = JsonFactory.JsonFromToken(keyValue.Value);
                        if (valueJson.ObjectType != JsonObjectType.Null)
                        {
                            objectsByKey.Add(keyValue.Key, valueJson);
                        }
                    }
                }
                return objectsByKey;
            }
        }

        /// <summary>Getter for a object at the given index.</summary>
        public IJsonObject GetArrayObject(int index)
        {
            return null;
        }

        /// <summary>Getter for a string at the given index.</summary>
        public string GetArrayString(int index)
        {
            return null;
        }

        /// <summary>Getter for a long integer at the given index.</summary>
        public long? GetArrayLong(int index)
        {
            return null;
        }

        /// <summary>Getter for a long integer at the given index.</summary>
        public double? GetArrayDouble(int index)
        {
            return null;
        }

        /// <summary>Getter for a boolean at the given index.</summary>
        public bool? GetArrayBoolean(int index)
        {
            return null;
        }

        /// <summary>Getter for a object with the given key.</summary>
        public IJsonObject GetDictionaryObject(string key)
        {
            return JsonFactory.JsonFromToken(m_jObject[key]);
        }

        /// <summary>Getter for a string with the given key.</summary>
        public string GetDictionaryString(string key)
        {
            return JsonFactory.StringFromToken(m_jObject[key]);
        }

        /// <summary>Getter for a long integer with the given key.</summary>
        public long? GetDictionaryLong(string key)
        {
            return JsonFactory.LongFromToken(m_jObject[key]);
        }

        /// <summary>Getter for a long integer with the given key.</summary>
        public double? GetDictionaryDouble(string key)
        {
            return JsonFactory.DoubleFromToken(m_jObject[key]);
        }

        /// <summary>Getter for a boolean with the given key.</summary>
        public bool? GetDictionaryBoolean(string key)
        {
            return JsonFactory.BooleanFromToken(m_jObject[key]);
        }

        #endregion
    }
}
