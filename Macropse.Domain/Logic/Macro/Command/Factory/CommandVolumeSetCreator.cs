using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System.Collections.Generic;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal class CommandVolumeSetCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandVolumeSet(
                value: (uint)parameters[0],
                type: CommandType.VolumeSet,
                repeats: repeats);
        }
    }
}
