using Macropse.Infrastructure.Module.Driver;

using System.Collections.Generic;

namespace Macropse.Domain.Logic.Interfaces
{
    public interface IMacros
    {
        string Name { get; }

        VirtualKey[] Keys { get; }

        List<IExecutable> Commands { get; }

        uint Repeats { get; }

        void Run(Device device);
    }
}
