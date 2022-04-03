using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System.Runtime.InteropServices;

namespace Macropse.Domain.Logic.Macro.Command
{
    public static class SystemVolumeManager
    {
        public enum VolumeUnit
        {
            Decibel,
            Scalar
        }

        [DllImport("SystemVolumeManager")]
        public static extern float GetSystemVolume(VolumeUnit vUnit);

        [DllImport("SystemVolumeManager")]
        public static extern void SetSystemVolume(double newVolume, VolumeUnit vUnit);


        [DllImport("SystemVolumeManager")]
        public static extern void MuteUnmute();
    }

    public class CommandVolumeAdd : CommandBase, IExecutable
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

        public CommandVolumeAdd(uint value, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams(value);
        }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                var curVolume = SystemVolumeManager.GetSystemVolume(SystemVolumeManager.VolumeUnit.Scalar);
                System.Console.WriteLine(curVolume);
                SystemVolumeManager.SetSystemVolume(curVolume + Params.Value, SystemVolumeManager.VolumeUnit.Scalar);
                System.Console.WriteLine(Params.Value);
            }
        }
    }
}
