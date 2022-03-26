using Macropse.Domain.Logic.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command
{
    public abstract class CommandBase 
    {
        protected CommandType Type { get; private set; }

        protected uint Repeats { get; private set; }

        public CommandBase(CommandType type, uint repeats = 1)
        {
            Type = type;
            Repeats = repeats;
        }
    }
}
