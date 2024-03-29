﻿using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System.Collections.Generic;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandSendKey : CommandBase, IExecutable
    {
        private class CommandParams
        {
            public Key[] Keys { get; }

            public CommandParams(Key[] keys)
            {
                Keys = keys;
            }
        }

        private CommandParams Params { get; }

        public CommandSendKey(List<Key> keys, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams(keys.ToArray());
        }

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                device.SendKeys(Params.Keys);
            }
        }
    }
}
