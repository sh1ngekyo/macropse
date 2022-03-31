using Macropse.Domain.Logic.Output;
using Macropse.Domain.Logic.Parser;
using Macropse.Infrastructure.Module.IO;

using System;

namespace Macropse.Presentation.Evaluator
{
    class Program
    {
        private static ExecutableModule BuildExecutableFromScript(Script script)
        {
            var output = new ScriptParser().Parse(script);
            if (output.HasError)
            {
                new ConsoleMessageSender().SendMessage(output.ErrorMessage);
                Console.ReadKey();
                Environment.Exit(0);
            }
            return output.Item;
        }

        private static void Main(string[] args)
        {
            var output = BuildExecutableFromScript(ScriptReader.ReadScript("script.mcr"));
            ProgramLoop.Run(output);
        }
    }
}
