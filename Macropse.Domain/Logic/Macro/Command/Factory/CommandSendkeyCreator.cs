using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System.Collections.Generic;
using System.Linq;

namespace Macropse.Domain.Logic.Macro.Command.Factory
{
    internal class CommandSendkeyCreator : CommandFactory
    {
        public override IExecutable Create(IList<dynamic> parameters, uint repeats)
        {
            return new CommandSendKey(
                keys: parameters.Cast<Key>().ToList(),
                type: CommandType.Run,
                repeats: repeats);
        }
    }
}
