using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Commands
{
    abstract class Command
    {
        public CommandType Type { get; private set; }

        public List<Param> Parameters { get; private set; }

        public uint Repeats { get; private set; }

        public Command(CommandType type, List<Param> parameters = null, uint repeats = 1)
        {
            Type = type;
            Parameters = parameters;
            Repeats = repeats;
        }
    }
}
