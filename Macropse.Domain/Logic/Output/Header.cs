using Macropse.Infrastructure.Module.Driver;

namespace Macropse.Domain.Logic.Output
{
    public class Header
    {
        public Header(VirtualKey pauseKey, int delay)
        {
            PauseKey = pauseKey;
            GlobalDelay = delay;
        }

        public VirtualKey PauseKey { get; }

        public int GlobalDelay { get; }
    }
}
