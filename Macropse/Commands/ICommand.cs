using System.Collections.Generic;

namespace macropse.Commands
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

    public interface ICommand
    {
        void Execute();
    }
}
