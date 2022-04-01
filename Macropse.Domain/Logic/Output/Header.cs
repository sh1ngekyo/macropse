using Macropse.Infrastructure.Module.Driver;

namespace Macropse.Domain.Logic.Output
{
    public class Header
    {
        public Header(VirtualKey pauseKey, int delay, bool whilePressed, string activeWindow)
        {
            PauseKey = pauseKey;
            GlobalDelay = delay;
            WhilePressed = whilePressed;
            ActiveWindow = activeWindow;
        }

        public VirtualKey PauseKey { get; }

        public int GlobalDelay { get; }

        public bool WhilePressed { get; }

        public string ActiveWindow { get; }
    }
}
