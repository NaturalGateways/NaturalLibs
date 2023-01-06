using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural.Json
{
    internal static class JsonFactory
    {
        /// <summary>Creates an object from a token.</summary>
        public static IJsonObject JsonFromToken(JToken token)
        {
            if (token == null)
                return JsonNullObject.Null;
            switch (token.Type)
            {
                case JTokenType.String:
                    return new JsonStringObject(token.Value<string>());
                case JTokenType.Integer:
                    return new JsonLongObject(token.Value<long>());
                case JTokenType.Float:
                    return new JsonDoubleObject(token.Value<double>());
                case JTokenType.Boolean:
                    return new JsonBooleanObject(token.Value<bool>());
                case JTokenType.Array:
                    return new JsonArrayObject((JArray)token);
                case JTokenType.Object:
                    return new JsonDictionaryObject((JObject)token);
            }
            return JsonNullObject.Null;
        }

        /// <summary>Getter for a string from the token.</summary>
        public static string StringFromToken(JToken token)
        {
            if (token == null)
                return null;
            switch (token.Type)
            {
                case JTokenType.String:
                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.Boolean:
                    return token.Value<string>();
            }
            return null;
        }

        /// <summary>Getter for a long integer from the token.</summary>
        public static long? LongFromToken(JToken token)
        {
            if (token == null)
                return null;
            switch (token.Type)
            {
                case JTokenType.String:
                    {
                        long intValue = 0;
                        if (Int64.TryParse(token.Value<string>(), out intValue))
                        {
                            return intValue;
                        }
                    }
                    break;
                case JTokenType.Integer:
                    return token.Value<long>();
            }
            return null;
        }

        /// <summary>Getter for a floating double from the token.</summary>
        public static double? DoubleFromToken(JToken token)
        {
            if (token == null)
                return null;
            switch (token.Type)
            {
                case JTokenType.String:
                    {
                        double doubleValue = 0;
                        if (Double.TryParse(token.Value<string>(), out doubleValue))
                        {
                            return doubleValue;
                        }
                    }
                    break;
                case JTokenType.Integer:
                    return token.Value<long>();
                case JTokenType.Float:
                    return token.Value<double>();
            }
            return null;
        }

        /// <summary>Getter for a boolean from the token.</summary>
        public static bool? BooleanFromToken(JToken token)
        {
            if (token == null)
                return null;
            switch (token.Type)
            {
                case JTokenType.String:
                    {
                        switch (token.Value<string>().ToLower())
                        {
                            case "false":
                                return false;
                            case "true":
                                return true;
                        }
                    }
                    break;
                case JTokenType.Boolean:
                    return token.Value<bool>();
            }
            return null;
        }
    }
}
