using System.Collections.Generic;

namespace macropse.Macros.Commands
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

    public enum ParamType
    {
        Key,
        Int,
        Bool,
        String,
        None
    }

    public interface ICommand
    {
        void Execute();
    }
}
