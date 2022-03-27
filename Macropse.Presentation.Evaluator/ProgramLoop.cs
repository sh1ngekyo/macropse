using Macropse.Infrastructure.Module.Driver;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Macropse.Presentation.Evaluator
{
    internal static class ProgramLoop
    {
        public static void Run(List<Domain.Logic.Macro.Macros> macros)
        {
            Device device = new Device();
            if(device.Load())
            {
                bool isRunning = true;
                while(isRunning)
                {
                    foreach(var macro in macros)
                    {
                        if(Keyboard.IsKeyDown(macro.Keys))
                        {
                            macro.Run(device);
                            Console.WriteLine("g");
                        }
                    }
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine("Error with driver");
        }
    }
}
