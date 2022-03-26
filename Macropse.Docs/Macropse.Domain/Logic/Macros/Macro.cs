using macropse.Macros.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macropse.Macros
{
    class Macro
    {
        public string Name { get; private set; }

        public List<ICommand> Commands { get; private set; }

        public uint Repeats { get; private set; }

        public Macro(string name, List<ICommand> commands, uint repeats = 1)
        {
            Name = name;
            Commands = commands;
            Repeats = repeats;
        }

        public void Run()
        {
            Commands.ForEach(x => x.Execute());
        }
    }
}
