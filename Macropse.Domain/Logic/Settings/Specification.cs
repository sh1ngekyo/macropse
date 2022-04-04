using Macropse.Domain.Logic.Macro.Command.Factory;
using Macropse.Infrastructure.Module.Driver;

using System;
using System.Collections.Generic;

namespace Macropse.Domain.Logic.Settings
{
    public static class Specification
    {
        public interface IValidParam
        {
            ParamType Type { get; }
            uint Count { get; }
        }

        public interface ICommandParamsInfo
        {
            List<IValidParam> ValidTypes { get; }
            (uint MinCount, uint MaxCount) Bounds { get; }
        }

        private class ValidParam : IValidParam
        {
            public ParamType Type { get; }

            public uint Count { get; }

            internal ValidParam(ParamType validType, uint count)
            {
                Type = validType;
                Count = count;
            }
        }

        private class CommandParamsInfo : ICommandParamsInfo
        {
            public List<IValidParam> ValidTypes { get; }

            public (uint MinCount, uint MaxCount) Bounds { get; }

            internal CommandParamsInfo(List<IValidParam> validTypes, (uint MinCount, uint MaxCount) bounds)
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
            { CommandType.Run, new CommandParamsInfo(new List<IValidParam>(2){ new ValidParam(ParamType.String, 1), new ValidParam(ParamType.Bool, 1) }, (1, 2))},
            { CommandType.ShowMsgBox, new CommandParamsInfo(new List<IValidParam>(1){ new ValidParam(ParamType.String, 1) }, (1, 1))},
            { CommandType.MoveMouseTo, new CommandParamsInfo(new List<IValidParam>(2){ new ValidParam(ParamType.Num, 2), new ValidParam(ParamType.Bool, 1) }, (2, 3))},
            { CommandType.Delay, new CommandParamsInfo(new List<IValidParam>(1){ new ValidParam(ParamType.Num, 1) }, (1, 1))},
            { CommandType.Sendkey, new CommandParamsInfo(new List<IValidParam>(1){ new ValidParam(ParamType.Key, 100) }, (1, 100))},
            { CommandType.Exit, null},
            { CommandType.LeftClick, null},
            { CommandType.RightClick, null},
            { CommandType.SendSignal, new CommandParamsInfo(new List<IValidParam>(1){ new ValidParam(ParamType.Num, 2) }, (1, 2))},
            { CommandType.MouseScroll, new CommandParamsInfo(new List<IValidParam>(1){ new ValidParam(ParamType.String, 1) }, (1, 1))},
            { CommandType.VolumeAdd, new CommandParamsInfo(new List<IValidParam>(1){ new ValidParam(ParamType.Num, 1) }, (1, 1))},
            { CommandType.VolumeRemove, new CommandParamsInfo(new List<IValidParam>(1){ new ValidParam(ParamType.Num, 1) }, (1, 1))},
            { CommandType.VolumeMute, null},
            { CommandType.VolumeSet, new CommandParamsInfo(new List<IValidParam>(1){ new ValidParam(ParamType.Num, 1) }, (1, 1))},
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
