using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandMoveMouseTo : CommandBase, IExecutable
    {
        private class CommandParams
        {
            public int X { get; }

            public int Y { get; }

            public bool UsePixels { get; }

            public CommandParams(int x, int y, bool usePixels)
            {
                X = x;
                Y = y;
                UsePixels = usePixels;
            }
        }

        private CommandParams Params { get; }

        public CommandMoveMouseTo(uint x, uint y, bool usePixels, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams((int)x, (int)y, usePixels);
        }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                device.MoveMouseTo(Params.X, Params.Y, Params.UsePixels);
            }
        }
    }
}
