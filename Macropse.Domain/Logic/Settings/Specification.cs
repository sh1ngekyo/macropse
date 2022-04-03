﻿using Macropse.Domain.Logic.Macro.Command.Factory;
using Macropse.Infrastructure.Module.Driver;

using System;
using System.Collections.Generic;

namespace Macropse.Domain.Logic.Settings
{
    public static class Specification
    {
        public interface ICommandParamsInfo
        {
            List<ParamType> ValidTypes { get; }
            (uint MinCount, uint MaxCount) Bounds { get; }
        }

        private class CommandParamsInfo : ICommandParamsInfo
        {
            public List<ParamType> ValidTypes { get; }

            public (uint MinCount, uint MaxCount) Bounds { get; }

            internal CommandParamsInfo(List<ParamType> validTypes, (uint MinCount, uint MaxCount) bounds)
            {
                ValidTypes = validTypes;
                Bounds = bounds;
            }
        }

        internal static readonly IReadOnlyDictionary<CommandType, CommandFactory> CommandTable = new Dictionary<CommandType, CommandFactory>()
        {
            { CommandType.Run, new CommandRunCreator() },
            { CommandType.ShowMsgBox, new CommandShowMsgBoxCreator() },
            { CommandType.MoveMouseTo, new CommandMoveMouseToCreator() },
            { CommandType.Delay, new CommandDelayCreator() },
            { CommandType.Sendkey, new CommandSendkeyCreator() },
            { CommandType.Exit, new CommandExitCreator() },
            { CommandType.LeftClick, new CommandLeftClickCreator() },
            { CommandType.RightClick, new CommandRightClickCreator() },
            { CommandType.SendSignal, new CommandSendSignalCreator() },
            { CommandType.MouseScroll, new CommandMouseScrollCreator() },
            { CommandType.VolumeAdd, new CommandVolumeAddCreator() },
            { CommandType.VolumeRemove, new CommandVolumeRemoveCreator() },
            { CommandType.VolumeMute, new CommandVolumeMuteCreator() },
            { CommandType.VolumeSet, new CommandVolumeSetCreator() },
        };

        internal static readonly IReadOnlyDictionary<CommandType, ICommandParamsInfo> ParamsTable = new Dictionary<CommandType, ICommandParamsInfo>()
        {
            { CommandType.Run, new CommandParamsInfo(new List<ParamType>(2){ParamType.String, ParamType.Bool}, (1, 2))},
            { CommandType.ShowMsgBox, new CommandParamsInfo(new List<ParamType>(1){ParamType.String}, (1, 1))},
            { CommandType.MoveMouseTo, new CommandParamsInfo(new List<ParamType>(2){ParamType.Num, ParamType.Num, ParamType.Bool}, (2, 3))},
            { CommandType.Delay, new CommandParamsInfo(new List<ParamType>(1){ParamType.Num}, (1, 1))},
            { CommandType.Sendkey, new CommandParamsInfo(new List<ParamType>(1){ParamType.Key}, (1, 1))},
            { CommandType.Exit, null},
            { CommandType.LeftClick, null},
            { CommandType.RightClick, null},
            { CommandType.SendSignal, new CommandParamsInfo(new List<ParamType>(2){ParamType.Num, ParamType.Num}, (1, 2))},
            { CommandType.MouseScroll, new CommandParamsInfo(new List<ParamType>(1){ParamType.String}, (1, 1))},
            { CommandType.VolumeAdd, new CommandParamsInfo(new List<ParamType>(1){ParamType.Num}, (1, 1))},
            { CommandType.VolumeRemove, new CommandParamsInfo(new List<ParamType>(1){ParamType.Num}, (1, 1))},
            { CommandType.VolumeMute, null},
            { CommandType.VolumeSet, new CommandParamsInfo(new List<ParamType>(1){ParamType.Num}, (1, 1))},
        };

        public static readonly IReadOnlyDictionary<ParamType, Type> ParamsTypeTable = new Dictionary<ParamType, Type>()
        {
            { ParamType.Num, typeof(uint)},
            { ParamType.Bool, typeof(bool)},
            { ParamType.Key, typeof(Key)},
            { ParamType.String, typeof(string)}
        };
    }
}
