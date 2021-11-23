using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Commands
{
    public enum ParamType
    {
        Integer,
        String,
        Key,
        Null
    }
    public class Param
    {
        public ParamType type { get; private set; }
    }
}
