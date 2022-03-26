using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Macros.Commands.Factory
{
    internal class CommandRunCreator : CommandFactory
    {
        public override ICommand Create(IList<object> parameters, uint repeats)
        {
            return new CommandRun(
                procName: (string)parameters[0],
                asAdmin: parameters.Count == 2 ? (bool)parameters[1] : false,
                type: CommandType.Run,
                repeats: repeats);
        }
    }
}
