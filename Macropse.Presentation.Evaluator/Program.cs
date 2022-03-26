using Macropse.Domain.External;
using Macropse.Domain.Logic.Parser;
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
        private static void Main(string[] args)
        {
            var input = ScriptReader.ReadScript("script.mcr");
            var output = new ScriptParser().Parse(input);
            if (output.HasError)
            {
                Console.WriteLine(output.ErrorMessage.Message);
                Console.ReadKey();
            }
            output.Item.ForEach(x => Console.WriteLine(x.Repeats));
            Console.ReadKey();
        }
    }
}
