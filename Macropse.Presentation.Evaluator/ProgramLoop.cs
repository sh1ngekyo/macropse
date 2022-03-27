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
        public static void Run(List<Domain.Logic.Macro.Macros> macros, int delay)
        {
            Device device = new Device();
            if(device.Load())
            {
                bool isRunning = true;
                int delta = delay;
                while(isRunning)
                {
                    foreach(var macro in macros)
                    {
                        if(delta > 0)
                        {
                            --delta;
                        }
                        if(Keyboard.IsKeyDown(macro.Keys) && delta == 0)
                        {
                            delta = delay;
                            Task.Run(() => macro.Run(device));
                        }
                    } 
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine("Error with driver");
        }
    }
}
