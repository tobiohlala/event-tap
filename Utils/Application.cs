using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EventTap.Utils
{
    internal class Application
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        /// <summary>
        /// Whether the application is currently focused.
        /// </summary>
        /// <returns><c>true</c> if the application is currently focused, else <c>false</c>.</returns>
        internal static bool HasFocus()
        {
            GetWindowThreadProcessId(GetForegroundWindow(), out int activeProcessId);

            return Process.GetCurrentProcess().Id == activeProcessId;
        }
    }
}
