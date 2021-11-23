using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Commands.Factory
{
    abstract class CommandFactory
    {
        public abstract ICommand Create(List<Param> parameters, uint repeats);
    }
}
