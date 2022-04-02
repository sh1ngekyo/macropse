using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandMouseScroll : CommandBase, IExecutable
    {
        private class CommandParams
        {
            public ScrollDirection Direction { get; }

            public CommandParams(ScrollDirection direction)
            {
                Direction = direction;
            }
        }

        private CommandParams Params { get; }

        public CommandMouseScroll(string direction, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            if (direction.Equals("up"))
            {
                Params = new CommandParams(ScrollDirection.Up);
            }
            else if (direction.Equals("down"))
            {
                Params = new CommandParams(ScrollDirection.Down);
            }
            else
            {
                Params = new CommandParams(ScrollDirection.None);
            }
        }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                if (Params.Direction != ScrollDirection.None)
                {
                    device.ScrollMouse(Params.Direction);
                }
            }
        }
    }
}
