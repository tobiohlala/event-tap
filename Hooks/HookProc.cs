using System;
using System.Runtime.InteropServices;

namespace EventTap.Hooks
{
    /// <summary>
    /// Callback registered by the <c>SetWindowsHookEx</c> function.<br/>
    /// See
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-hookproc">
    /// HOOKPROC Callback function</a> for more details.
    /// </summary>
    /// <param name="nCode"></param>
    /// <param name="wParam"></param>
    /// <param name="lParam"></param>
    /// <returns></returns>
    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    internal delegate long HookProc(int nCode, IntPtr wParam, IntPtr lParam);
}