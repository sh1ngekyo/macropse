using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Commands.Factory
{
    class CommandRunCreator : CommandFactory
    {
        public override ICommand Create(List<Param> parameters, uint repeats)
        {
            //обработать параметры!!!!
            return new CommandRun(CommandType.Run, parameters, repeats);
        }
    }
}
