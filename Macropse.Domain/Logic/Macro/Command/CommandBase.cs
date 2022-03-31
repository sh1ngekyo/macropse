using Macropse.Domain.Logic.Settings;

namespace Macropse.Domain.Logic.Macro.Command
{
    public abstract class CommandBase 
    {
        protected CommandType Type { get; private set; }

        protected uint Repeats { get; private set; }

        public CommandBase(CommandType type, uint repeats = 1)
        {
            Type = type;
            Repeats = repeats;
        }
    }
}
