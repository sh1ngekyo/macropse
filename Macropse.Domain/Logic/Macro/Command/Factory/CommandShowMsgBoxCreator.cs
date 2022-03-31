using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System.Collections.Generic;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    class CommandShowMsgBoxCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandShowMsgBox(
                text: (string)parameters[0],
                type: CommandType.ShowMsgBox,
                repeats: repeats);
        }
    }
}
