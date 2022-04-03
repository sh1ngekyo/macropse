using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal class CommandVolumeRemoveCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandVolumeRemove(
                value: (uint)parameters[0],
                type: CommandType.VolumeRemove,
                repeats: repeats);
        }
    }
}
