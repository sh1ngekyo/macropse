using macropse.Commands.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Commands
{
    abstract class Specification
    {
        public static readonly Dictionary<CommandType, CommandFactory> CommandTable = new Dictionary<CommandType, CommandFactory>()
        {
            { CommandType.Run, new CommandRunCreator() }
        };
    }
}
