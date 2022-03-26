using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Interfaces
{
    public interface IMacros
    {
        string Name { get; }

        List<IExecutable> Commands { get; }

        uint Repeats { get; }

        void Run();
    }
}
