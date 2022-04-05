using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.Driver
{
    public enum ScrollDirection
    {
        Up = 0,
        Down = 1,
        None,
    }

    public class MousePressedEventArgs : EventArgs
    {
        public MouseState State { get; set; }
        public bool Handled { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public short Rolling { get; set; }
    }
}
