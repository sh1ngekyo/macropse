using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

namespace Macropse.Domain.Logic.Macro.Command
{
    class CommandLeftClick : CommandBase, IExecutable
    {
        public CommandLeftClick(CommandType type, uint repeats) : base(type, repeats) { }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                device.SendLeftClick();
            }
        }
    }
}
