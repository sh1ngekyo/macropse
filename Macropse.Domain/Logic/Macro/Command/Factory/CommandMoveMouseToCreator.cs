﻿using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System.Collections.Generic;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal class CommandMoveMouseToCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandMoveMouseTo(
                x: (uint)parameters[0],
                y: (uint)parameters[1],
                usePixels: parameters.Count == 3 ? (bool)parameters[2] : true,
                type: CommandType.MoveMouseTo,
                repeats: repeats);;
        }
    }
}
