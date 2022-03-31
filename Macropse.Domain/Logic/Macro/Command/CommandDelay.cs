using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System.Threading;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandDelay : CommandBase, IExecutable
    {
        private class CommandParams
        {
            public int Value { get; }

            public CommandParams(int value)
            {
                Value = value;
            }
        }

        private CommandParams Params { get; }

        public CommandDelay(uint value, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams((int)value);
        }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                Thread.Sleep(Params.Value);
            }
        }
    }
}
