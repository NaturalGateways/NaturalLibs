using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Json
{
    internal class JsonNullObject : IJsonObject
    {
        #region Base

        /// <summary>Singleton object.</summary>
        private static JsonNullObject s_singleton = new JsonNullObject();

        /// <summary>Singleton object.</summary>
        public static JsonNullObject Null { get { return s_singleton; } }

        #endregion

        #region IJsonObject implementation

        /// <summary>Getter for the object type.</summary>
        public JsonObjectType ObjectType { get { return JsonObjectType.Null; } }

        /// <summary>Getter for the object as a string.</summary>
        public string AsString { get { return null; } }
        /// <summary>Getter for the object as a long integer.</summary>
        public long? AsLong { get { return null; } }
        /// <summary>Getter for the object as a double float.</summary>
        public double? AsDouble { get { return null; } }
        /// <summary>Getter for the object as a boolean.</summary>
        public bool? AsBoolean { get { return null; } }
        /// <summary>Getter for the object as an array of object.</summary>
        public IJsonObject[] AsObjectArray { get { return null; } }
        /// <summary>Getter for the object as a boolean.</summary>
        public Dictionary<string, IJsonObject> AsObjectDictionary { get { return null; } }

        /// <summary>Getter for a object at the given index.</summary>
        public IJsonObject GetArrayObject(int index)
        {
            return JsonNullObject.Null;
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
            return JsonNullObject.Null;
        }

        /// <summary>Getter for a string with the given key.</summary>
        public string GetDictionaryString(string key)
        {
            return null;
        }

        /// <summary>Getter for a long integer with the given key.</summary>
        public long? GetDictionaryLong(string key)
        {
            return null;
        }

        /// <summary>Getter for a long integer with the given key.</summary>
        public double? GetDictionaryDouble(string key)
        {
            return null;
        }

        /// <summary>Getter for a boolean with the given key.</summary>
        public bool? GetDictionaryBoolean(string key)
        {
            return null;
        }

        #endregion
    }
}
