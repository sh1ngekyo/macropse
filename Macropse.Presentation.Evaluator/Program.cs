using Macropse.Domain.External;
using Macropse.Domain.Logic.Parser;
using Macropse.Infrastructure.Module.Driver;
using Macropse.Infrastructure.Module.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Presentation.Evaluator
{
    class Program
    {
        private static List<Domain.Logic.Macro.Macros> CreateMacros(string path)
        {
            var input = ScriptReader.ReadScript(path);
            var output = new ScriptParser().Parse(input);
            if (output.HasError)
            {
                Console.WriteLine(output.ErrorMessage.Message);
                return null;
            }
            return output.Item;
        }

        private static void Main(string[] args)
        {
            var macros = CreateMacros("script.mcr");
            if (macros != null)
            {
                ProgramLoop.Run(macros, 100);
            }
            Console.ReadKey();
        }
    }
}
