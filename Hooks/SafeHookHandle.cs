using System;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace EventTap.Hooks
{
    /// <summary>
    /// Wrapper to safely store a hook handle.
    /// </summary>
    internal class SafeHookHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Removes a hook procedure from the hook chain previously installed
        /// by the <c>SetWindowsHookEx</c> function.<br/>
        /// See
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex">
        /// UnhookWindowsHookEx</a> for more details.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr handle);

        public SafeHookHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            return UnhookWindowsHookEx(handle);
        }
    }
}
