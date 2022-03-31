using System;
using System.Collections.Generic;
using System.Linq;

namespace Macropse.Domain.Logic.Parser
{
    public static class ParserUtills
    {
        public static bool ToEnum<T>(this string value, out T result)
        {
            result = default(T);
            try
            {
                result = (T)Enum.Parse(typeof(T), value, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<string> ExtractRawParams(this string xmlAttributeValue)
        {
            return string.IsNullOrEmpty(xmlAttributeValue) ? null : xmlAttributeValue.ToLower().Split(new char[] { ',' }).ToList();
        }

        public static bool TryToParam<T>(string raw, out T parsedParam)
        {
            parsedParam = default(T);

            if (typeof(T).Equals(typeof(Infrastructure.Module.Driver.Key)) || typeof(T).Equals(typeof(Infrastructure.Module.Driver.VirtualKey)))
            {
                foreach(var c in raw)
                {
                    if(!char.IsLetterOrDigit(c))
                    {
                        return false;
                    }
                }
                return raw.ToEnum<T>(out parsedParam);
            }

            try
            {
                parsedParam = (T)Convert.ChangeType(raw, typeof(T));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryToValidateKeyword(string keyword, string[] allowedKeywords)
        {
            if (allowedKeywords.Contains(keyword))
            {
                return true;
            }
            return false;
        }
    }
}
