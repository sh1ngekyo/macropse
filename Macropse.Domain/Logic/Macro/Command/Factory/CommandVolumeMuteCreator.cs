using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal class CommandVolumeMuteCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandVolumeMute(
                type: CommandType.VolumeMute,
                repeats: repeats);
        }
    }
}
