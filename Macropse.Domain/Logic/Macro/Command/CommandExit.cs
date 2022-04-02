using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandExit : CommandBase, IExecutable
    {
        public CommandExit(CommandType type) : base(type, 1) { }

        public void Execute(Device device) => Environment.Exit(0);
    }
}
