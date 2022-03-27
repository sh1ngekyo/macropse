using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandMoveMouseTo : CommandBase, IExecutable
    {
        private class CommandParams
        {
            public int X { get; }
            public int Y { get; }

            public CommandParams(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private CommandParams Params { get; }

        public CommandMoveMouseTo(uint x, uint y, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams((int)x, (int)y);
        }

        public void Execute(Device device)
        {
            for (int i = 0; i < Repeats; ++i)
            {
                device.MoveMouseTo(Params.X, Params.Y);
            }
        }
    }
}
