using Macropse.Infrastructure.Module.Driver;

using System;
using System.Runtime.InteropServices;

namespace Macropse.Presentation.Evaluator
{
    static class Keyboard
    {
        /// <summary>
        /// Contains keyboard state codes
        /// </summary>
        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }

        /// <summary>
        /// Import static function GetKeyState from user32.dll
        /// </summary>
        /// <param name="keyCode"> code of current key</param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        /// <summary>
        /// Get state for current key
        /// </summary>
        /// <param name="key"> current key</param>
        private static KeyStates GetKeyState(VirtualKey key)
        {
            KeyStates state = KeyStates.None;
            short retVal = GetKeyState((int)key);
            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;
            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;
            return state;
        }

        /// <summary>
        /// Check if key pressed
        /// </summary>
        /// <param name="key"> current key</param>
        public static bool IsKeyDown(params VirtualKey[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
            {
                if (KeyStates.Down != (GetKeyState(keys[i]) & KeyStates.Down))
                {
                    return false;
                }
            }
            return true; 
        }

        /// <summary>
        /// Check if key released
        /// </summary>
        /// <param name="key"> current key</param>
        public static bool IsKeyToggled(params VirtualKey[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
            {
                if (KeyStates.Toggled != (GetKeyState(keys[i]) & KeyStates.Toggled))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
