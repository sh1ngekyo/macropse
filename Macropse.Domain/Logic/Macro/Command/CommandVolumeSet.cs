using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Macro.Command.Utils;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandVolumeSet : CommandBase, IExecutable
    {
        private class CommandParams
        {
            public float Value { get; }

            public CommandParams(float value)
            {
                Value = value / 100;
            }
        }

        private CommandParams Params { get; }

        public CommandVolumeSet(uint value, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams(value);
        }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                SystemVolumeUtil.SetSystemVolume(Params.Value, SystemVolumeUtil.VolumeUnit.Scalar);
            }
        }
    }
}
