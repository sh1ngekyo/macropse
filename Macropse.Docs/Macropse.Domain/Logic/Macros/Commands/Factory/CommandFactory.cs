using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Macros.Commands.Factory
{
    internal abstract class CommandFactory
    {
        public abstract ICommand Create(IList<object> parameters, uint repeats);
    }
}
