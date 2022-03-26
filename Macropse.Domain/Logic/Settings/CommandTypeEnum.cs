using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Settings
{
    public enum CommandType
    {
        Run,
        Sendkey,
        Delay,
        MouseClick,
        MoveMouseTo,
        ShowMsgBox,
        Close,
        Minimize,
        Maximize,
        VolumeAdd,
        VolumeMax,
        VolumeMin
    }
}
