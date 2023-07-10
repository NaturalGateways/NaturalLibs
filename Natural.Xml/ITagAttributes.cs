using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Xml
{
    /// <summary>Interface for the object passed in to read attributes.</summary>
    public interface ITagAttributes
    {
        /// <summary>Getter for a string, throws an exception if it is missing.</summary>
        string GetString(string name);
        /// <summary>Getter for a long integer, throws an exception if it is missing or invalid.</summary>
        long GetLong(string name);
        /// <summary>Getter for an enum, throws an exception if it is missing or invalid.</summary>
        EnumType GetEnum<EnumType>(string name) where EnumType : struct, IConvertible;

        /// <summary>Getter for a string, returns null if it is missing.</summary>
        string? GetNullableString(string name);
        /// <summary>Getter for a long integer, returns null if it is missing or invalid.</summary>
        long? GetNullableLong(string name);
        /// <summary>Getter for an enum, returns null if it is missing or invalid.</summary>
        EnumType? GetNullableEnum<EnumType>(string name) where EnumType : struct, IConvertible;
    }
}
