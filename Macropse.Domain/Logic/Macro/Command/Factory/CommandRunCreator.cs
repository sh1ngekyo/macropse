using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal class CommandRunCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandRun(
                procName: (string)parameters[0],
                asAdmin: parameters.Count == 2 ? (bool)parameters[1] : false,
                type: CommandType.Run,
                repeats: repeats);
        }
    }
}
