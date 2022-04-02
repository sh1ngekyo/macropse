using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandSendSignal : CommandBase, IExecutable
    {
        private class CommandParams
        {
            private const int MIN_VALUE = 0x25;
            private const int MAX_VALUE = 0x7FFF;

            private const int MIN_DURATION = 1;

            public int Value { get; }

            public int Duration { get; }

            public CommandParams(int value, int duration)
            {
                Value = value < MIN_VALUE ? MIN_VALUE : value > MAX_VALUE ? MAX_VALUE : value;
                Duration = duration < MIN_DURATION ? MIN_DURATION : duration;
            }
        }

        private CommandParams Params { get; }

        public CommandSendSignal(int value, int duration, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams(value, duration);
        }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                Console.Beep(Params.Value, Params.Duration);
            }
        }
    }
}
