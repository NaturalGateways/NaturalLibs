using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Json
{
    public enum JsonObjectType
    {
        Null,
        String,
        Long,
        Double,
        Boolean,
        Array,
        Dictionary
    }

    public interface IJsonObject
    {
        /// <summary>Getter for the object type.</summary>
        JsonObjectType ObjectType { get; }

        /// <summary>Getter for the object as a string.</summary>
        string AsString { get; }
        /// <summary>Getter for the object as a long integer.</summary>
        long? AsLong { get; }
        /// <summary>Getter for the object as a double float.</summary>
        double? AsDouble { get; }
        /// <summary>Getter for the object as a boolean.</summary>
        bool? AsBoolean { get; }
        /// <summary>Getter for the object as an array of object.</summary>
        IJsonObject[] AsObjectArray { get; }
        /// <summary>Getter for the object as a boolean.</summary>
        Dictionary<string, IJsonObject> AsObjectDictionary { get; }

        /// <summary>Getter for a object at the given index.</summary>
        IJsonObject GetArrayObject(int index);
        /// <summary>Getter for a string at the given index.</summary>
        string GetArrayString(int index);
        /// <summary>Getter for a long integer at the given index.</summary>
        long? GetArrayLong(int index);
        /// <summary>Getter for a long integer at the given index.</summary>
        double? GetArrayDouble(int index);
        /// <summary>Getter for a boolean at the given index.</summary>
        bool? GetArrayBoolean(int index);

        /// <summary>Getter for a object with the given key.</summary>
        IJsonObject GetDictionaryObject(string key);
        /// <summary>Getter for a string with the given key.</summary>
        string GetDictionaryString(string key);
        /// <summary>Getter for a long integer with the given key.</summary>
        long? GetDictionaryLong(string key);
        /// <summary>Getter for a long integer with the given key.</summary>
        double? GetDictionaryDouble(string key);
        /// <summary>Getter for a boolean with the given key.</summary>
        bool? GetDictionaryBoolean(string key);
    }
}
