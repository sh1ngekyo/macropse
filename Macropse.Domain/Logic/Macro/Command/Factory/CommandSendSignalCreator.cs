using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal class CommandSendSignalCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandSendSignal(
                value: (int)parameters[0],
                duration: parameters.Count == 2 ? (int)parameters[1] : 1000,
                type: CommandType.SendSignal,
                repeats: repeats);
        }
    }
}
