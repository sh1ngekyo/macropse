using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System.Diagnostics;

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

        public void Execute(Device device)
        {
            for (var i = 0; i < Repeats; ++i)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = Params.ProcessName;
                proc.StartInfo.UseShellExecute = true;
                if (Params.RunAsAdmin)
                {
                    proc.StartInfo.Verb = "runas";
                }
                proc.Start();
            }
        }
    }
}
