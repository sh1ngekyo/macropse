using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Parser
{
    internal static class ParserUtills
    {
        internal static bool ToEnum<T>(this string value, out T result)
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

        internal static List<string> ExtractRawParams(this string xmlAttributeValue)
        {
            return string.IsNullOrEmpty(xmlAttributeValue) ? null : xmlAttributeValue.ToLower().Split(new char[] { ',' }).ToList();
        }

        public static bool TryToParam<T>(string raw, out T parsedParam)
        {
            parsedParam = default(T);
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
    }
}
