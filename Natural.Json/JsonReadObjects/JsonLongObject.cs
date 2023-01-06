﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Natural.Json
{
    internal class JsonLongObject : IJsonObject
    {
        #region Base

        /// <summary>The integer value.</summary>
        private long m_longValue = 0;

        /// <summary>Constructor.</summary>
        public JsonLongObject(long longValue)
        {
            m_longValue = longValue;
        }

        #endregion

        #region IJsonObject implementation

        /// <summary>Getter for the object type.</summary>
        public JsonObjectType ObjectType { get { return JsonObjectType.Long; } }

        /// <summary>Getter for the object as a string.</summary>
        public string AsString { get { return m_longValue.ToString(); } }
        /// <summary>Getter for the object as a long integer.</summary>
        public long? AsLong { get { return m_longValue; } }
        /// <summary>Getter for the object as a double float.</summary>
        public double? AsDouble { get { return m_longValue; } }
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
