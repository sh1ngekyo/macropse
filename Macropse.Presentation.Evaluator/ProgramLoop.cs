using Macropse.Domain.Logic.Output;
using Macropse.Infrastructure.Module.Driver;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Macropse.Presentation.Evaluator
{
    internal static class ProgramLoop
    {
        public static void Run(ExecutableModule executable)
        {
            Device device = new Device();
            if(device.Load())
            {
                bool isRunning = true;
                var delta = executable.Root.GlobalDelay;
                while (isRunning)
                {
                    foreach(var macro in executable.Macros)
                    {
                        if(delta > 0)
                        {
                            --delta;
                        }
                        if(Keyboard.IsKeyDown(macro.Keys) && delta == 0)
                        {
                            delta = executable.Root.GlobalDelay;
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
