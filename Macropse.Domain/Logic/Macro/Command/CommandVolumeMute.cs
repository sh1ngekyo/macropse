using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Macro.Command.Utils;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandVolumeMute : CommandBase, IExecutable
    {
        public CommandVolumeMute(CommandType type, uint repeats = 1) : base(type, repeats) { }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                SystemVolumeUtil.MuteUnmute();
            }
        }
    }
}
