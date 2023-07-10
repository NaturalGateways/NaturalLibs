using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Xml
{
    internal class XmlAttributes : ITagAttributes
    {
        #region Base

        /// <summary>The XML reader.</summary>
        private System.Xml.XmlReader m_reader = null;

        /// <summary>Constructor.</summary>
        public XmlAttributes(System.Xml.XmlReader reader)
        {
            m_reader = reader;
        }

        #endregion

        #region ITagAttributes implementation

        /// <summary>Getter for a string, throws an exception if it is missing.</summary>
        public string GetString(string name)
        {
            string? attributeValue = m_reader.GetAttribute(name);
            if (attributeValue == null)
                throw new Exception($"Tag '{m_reader.Name}' has no attribute '{name}'.");
            return attributeValue;
        }

        /// <summary>Getter for a long integer, throws an exception if it is missing or invalid.</summary>
        public long GetLong(string name)
        {
            string? attributeValue = m_reader.GetAttribute(name);
            if (attributeValue == null)
                throw new Exception($"Tag '{m_reader.Name}' has no attribute '{name}'.");
            long longValue = 0;
            if (Int64.TryParse(attributeValue, out longValue))
                return longValue;
            throw new Exception($"Tag '{m_reader.Name}' has invalid attribute '{name}': '{attributeValue}'");
        }

        /// <summary>Getter for an enum, throws an exception if it is missing or invalid.</summary>
        public EnumType GetEnum<EnumType>(string name) where EnumType : struct, IConvertible
        {
            string? attributeValue = m_reader.GetAttribute(name);
            if (attributeValue == null)
                throw new Exception($"Tag '{m_reader.Name}' has no attribute '{name}'.");
            EnumType enumValue = default(EnumType);
            if (Enum.TryParse<EnumType>(attributeValue, out enumValue))
                return enumValue;
            throw new Exception($"Tag '{m_reader.Name}' has invalid attribute '{name}': '{attributeValue}'");
        }

        /// <summary>Getter for a string, returns null if it is missing.</summary>
        public string? GetNullableString(string name)
        {
            string? attributeValue = m_reader.GetAttribute(name);
            if (attributeValue == null)
                return null;
            return attributeValue;
        }

        /// <summary>Getter for a long integer, returns null if it is missing or invalid.</summary>
        public long? GetNullableLong(string name)
        {
            string? attributeValue = m_reader.GetAttribute(name);
            if (attributeValue == null)
                return null;
            long longValue = 0;
            if (Int64.TryParse(attributeValue, out longValue))
                return longValue;
            return null;
        }

        /// <summary>Getter for an enum, returns null if it is missing or invalid.</summary>
        public EnumType? GetNullableEnum<EnumType>(string name) where EnumType : struct, IConvertible
        {
            string? attributeValue = m_reader.GetAttribute(name);
            if (attributeValue == null)
                return null;
            EnumType enumValue = default(EnumType);
            if (Enum.TryParse<EnumType>(attributeValue, out enumValue))
                return enumValue;
            return null;
        }

        #endregion
    }
}
