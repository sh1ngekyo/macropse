using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using macropse.Macros.Commands;

namespace macropse.Parser
{
    public static class AttributeParser
    {
        public static bool TryParseParam<T>(string raw, out T parsedParam)
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
