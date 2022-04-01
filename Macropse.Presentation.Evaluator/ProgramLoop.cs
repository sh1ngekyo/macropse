using Macropse.Domain.Logic.Output;
using Macropse.Infrastructure.Module.Driver;
using Macropse.Presentation.Evaluator.Utils;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Macropse.Presentation.Evaluator
{
    internal class ProgramLoop
    {
        public ProgramLoop(ExecutableModule executable)
        {
            this.executable = executable;
            cancelSource = new CancellationTokenSource();
            device = new Device();
        }

        private Device device;

        private CancellationTokenSource cancelSource;

        private ExecutableModule executable;

        private void ProcessKeyboardInput()
        {
            executable.Macros.ForEach(macro =>
            {
                if (Keyboard.IsKeyDown(macro.Keys) && !macro.Locked)
                {
                    if (executable.Root.ActiveWindow.WindowIsActive())
                    {
                        Task.Run(() => macro.Run(device));
                        cancelSource.Token.WaitHandle.WaitOne(executable.Root.GlobalDelay);
                    }
                }
            });
        }

        private void WaitWhile<T>(int ms, Func<T[], bool> whileEvent, params T[] eventArg)
        {
            while (whileEvent.Invoke(eventArg))
            {
                cancelSource.Token.WaitHandle.WaitOne(ms);
            }
        }

        public void Run()
        {
            if (device.Load())
            {
                var isRunning = true;
                var paused = false;
                while (isRunning)
                {
                    if (Keyboard.IsKeyDown(executable.Root.PauseKey))
                    {
                        paused = !(paused is true);
                        WaitWhile(1, Keyboard.IsKeyDown, executable.Root.PauseKey);
                    }
                    if (!paused)
                    {
                        ProcessKeyboardInput();
                        if (!executable.Root.WhilePressed)
                        {
                            executable.Macros.ForEach(x =>
                            {
                                WaitWhile(1, Keyboard.IsKeyDown, x.Keys);
                            });
                        }
                    }
                    cancelSource.Token.WaitHandle.WaitOne(1);
                }
            }
            Console.WriteLine("Error with driver");
        }
    }
}
