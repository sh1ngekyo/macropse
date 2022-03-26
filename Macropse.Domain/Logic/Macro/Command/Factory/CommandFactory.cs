using Macropse.Domain.Logic.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal abstract class CommandFactory
    {
        public abstract IExecutable Create(IList<dynamic> parameters, uint repeats);
    }
}
