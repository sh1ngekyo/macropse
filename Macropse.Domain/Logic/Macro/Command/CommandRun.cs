using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandRun : CommandBase, IExecutable
    {
        private class CommandParams
        {
            public string ProcessName { get; }
            public bool RunAsAdmin { get; }

            public CommandParams(string processName, bool runAsAdmin)
            {
                ProcessName = processName;
                RunAsAdmin = runAsAdmin;
            }
        }

        private CommandParams Params { get; }

        public CommandRun(string procName, bool asAdmin, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams(procName, asAdmin);
        }

        public void Execute()
        {
            System.Console.WriteLine($"Execute {typeof(CommandRun)} with param 1: {Params.ProcessName} and param 2: {Params.RunAsAdmin}");
        }
    }
}
