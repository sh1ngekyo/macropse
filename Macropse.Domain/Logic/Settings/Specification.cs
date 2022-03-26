using Macropse.Domain.Logic.Macro.Command.Factory;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Settings
{
    public static class Specification
    {
        public interface ICommandParamsInfo
        {
            List<ParamType> ValidTypes { get; }
            Tuple<uint, uint> Bounds { get; }
        }

        private class CommandParamsInfo : ICommandParamsInfo
        {
            public List<ParamType> ValidTypes { get; }

            public Tuple<uint, uint> Bounds { get; }

            internal CommandParamsInfo(List<ParamType> validTypes, Tuple<uint, uint> bounds)
            {
                ValidTypes = validTypes;
                Bounds = bounds;
            }
        }

        internal static readonly IReadOnlyDictionary<CommandType, CommandFactory> CommandTable = new Dictionary<CommandType, CommandFactory>()
        {
            { CommandType.Run, new CommandRunCreator() }
        };

        internal static readonly IReadOnlyDictionary<CommandType, ICommandParamsInfo> ParamsTable = new Dictionary<CommandType, ICommandParamsInfo>()
        {
            { CommandType.Run, new CommandParamsInfo(new List<ParamType>(2){ParamType.String, ParamType.Bool}, new Tuple<uint, uint>(1, 2))}
        };

        public static readonly IReadOnlyDictionary<ParamType, Type> ParamsTypeTable = new Dictionary<ParamType, Type>()
        {
            { ParamType.Num, typeof(uint)},
            { ParamType.Bool, typeof(bool)},
            { ParamType.Key, typeof(object)},
            { ParamType.String, typeof(string)},
            { ParamType.None, null}
        };
    }
}
