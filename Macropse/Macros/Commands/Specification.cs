using macropse.Macros.Commands.Factory;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Macros.Commands
{
    static class Specification
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

        public static readonly IReadOnlyDictionary<CommandType, CommandFactory> CommandTable = new Dictionary<CommandType, CommandFactory>()
        {
            { CommandType.Run, new CommandRunCreator() }
        };

        public static readonly IReadOnlyDictionary<CommandType, ICommandParamsInfo> ParamsTable = new Dictionary<CommandType, ICommandParamsInfo>()
        {
            { CommandType.Run, new CommandParamsInfo(new List<ParamType>(2){ParamType.String, ParamType.Bool}, new Tuple<uint, uint>(1, 2))}
        };

        public static readonly IReadOnlyDictionary<ParamType, Type> ParamsTypeTable = new Dictionary<ParamType, Type>()
        {
            { ParamType.Int, typeof(uint)},
            { ParamType.Bool, typeof(bool)},
            { ParamType.Key, typeof(object)},
            { ParamType.String, typeof(string)},
            { ParamType.None, null}
        };
    }
}
