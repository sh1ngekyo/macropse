﻿using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System.Collections.Generic;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal class CommandExitCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandExit(
                type: CommandType.Exit);
        }
    }
}
