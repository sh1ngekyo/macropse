using Macropse.Domain.Logic.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro
{
    public sealed class Macros : IMacros
    {
        public string Name { get; }

        public List<IExecutable> Commands { get; }

        public uint Repeats { get; private set; }

        public Macros(string name, List<IExecutable> commands, uint repeats = 1)
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
