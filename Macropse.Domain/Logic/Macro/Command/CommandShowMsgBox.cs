using Macropse.Domain.Logic.Interfaces;
using Macropse.Domain.Logic.Settings;
using Macropse.Infrastructure.Module.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Macropse.Domain.Logic.Macro.Command
{
    public class CommandShowMsgBox : CommandBase, IExecutable
    {
        private class CommandParams
        {
            public string Text { get; }

            public CommandParams(string text)
            {
                Text = text;
            }
        }

        private CommandParams Params { get; }

        public CommandShowMsgBox(string text, CommandType type, uint repeats = 1) : base(type, repeats)
        {
            Params = new CommandParams(text);
        }

        public void Execute(Device device)
        {
            for (int i = 0; i < Repeats; ++i)
            {
                MessageBox.Show(Params.Text);
            }
        }
    }
}
