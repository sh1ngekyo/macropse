using Macropse.Domain.Logic.Interfaces;
using Macropse.Infrastructure.Module.Driver;

using System.Collections.Generic;

namespace Macropse.Domain.Logic.Macro
{
    public sealed class Macros : IMacros
    {
        public string Name { get; }

        public List<IExecutable> Commands { get; }

        public uint Repeats { get; private set; }

        public VirtualKey[] Keys { get; }

        public bool Locked { get; private set; }

        public Macros(string name, List<VirtualKey> keys, List<IExecutable> commands, uint repeats = 1)
        {
            Name = name;
            Keys = keys.ToArray();
            Commands = commands;
            Repeats = repeats;
            Locked = false;
        }

        public void Run(Device device)
        {
            Locked = true;
            for (int i = 0; i < Repeats; ++i)
            {
                Commands.ForEach(x => x.Execute(device: device));
            }
            Locked = false;
        }
    }
}
