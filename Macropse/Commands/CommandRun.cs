using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Commands
{
    class CommandRun : Command, ICommand
    {
        public CommandRun(CommandType type, List<Param> parameters = null, uint repeats = 1) : base(type, parameters, repeats)
        {

        }
        public void Execute()
        {
            System.Diagnostics.Debug.WriteLine($"Execute {typeof(CommandRun)}");
        }
    }
}
