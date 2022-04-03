using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command.Utils
{
    internal static class SystemVolumeUtil
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
}
