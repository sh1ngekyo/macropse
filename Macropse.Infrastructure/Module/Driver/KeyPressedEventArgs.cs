using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Infrastructure.Module.Driver
{
    public class KeyPressedEventArgs : EventArgs
    {
        public Key Key { get; set; }
        public KeyState State { get; set; }
        public bool Handled { get; set; }
    }
}
