using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            for (int i = 0; i < Repeats; ++i)
            {
                Thread.Sleep(Params.Value);
            }
        }
    }
}
