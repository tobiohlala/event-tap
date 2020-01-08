using System;
using System.Runtime.InteropServices;

namespace EventTap.Hooks
{
    public partial class KeyboardHook
    {
        /// <summary>
        /// Contains information about low-level keyboard input events.<br/>
        /// See
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-kbdllhookstruct">
        /// KBDLLHOOKSTRUCT</a> for more details.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct LowLevelKeyboardHookStruct
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        /// <summary>
        /// Passes the hook information to the next hook in the hook chain.<br/>
        /// See
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex">
        /// CallNextHookEx</a> for more details.
        /// </summary>
        /// <param name="hhk"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        /// <remarks>
        /// Calling CallNextHookEx is optional, but it is highly recommended;
        /// otherwise, other applications that have installed hooks will not
        /// receive hook notifications and may behave incorrectly as a result.
        /// You should call CallNextHookEx unless you absolutely need to prevent
        /// the notification from being seen by other applications.
        /// </remarks>
        [DllImport("user32.dll", CallingConvention = CallingConvention.Winapi)]
        private static extern long CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    }
}