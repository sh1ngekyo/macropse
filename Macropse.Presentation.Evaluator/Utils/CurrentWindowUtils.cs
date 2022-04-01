using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Macropse.Presentation.Evaluator.Utils
{
    public static class CurrentWindowUtils
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private static string GetActiveProcessFileName()
        {
            var hwnd = GetForegroundWindow();
            GetWindowThreadProcessId(hwnd, out var pid);
            var p = Process.GetProcessById((int)pid);
            return p.MainModule.FileName;
        }

        public static bool WindowIsActive(this string procName)
        {
            return string.IsNullOrWhiteSpace(procName) ? true : procName.Equals(System.IO.Path.GetFileName(GetActiveProcessFileName()));
        }
    }
}
