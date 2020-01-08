using System;
using System.Runtime.InteropServices;

using EventTap.Events;
using EventTap.Utils;

namespace EventTap.Hooks
{
    /// <summary>
    /// This hook enables you to monitor keyboard input events about to be posted
    /// in a thread input queue.
    /// </summary>
    public partial class KeyboardHook : Hook, IKeyboardEvents, IEventTapSource<IKeyboardEvents>
    {
        public override bool ShouldIgnoreApplicationFocus { get; set; } = false;

        public override bool ShouldPreventNextHook { get; set; } = false;

        internal override HookType Type => HookType.LowLevelKeyboardHook;

        /// <summary>
        /// Processes the received windows keyboard message and raises the
        /// corresponding <see cref="IKeyboardEvents"/>.<br/>
        /// The system calls this function every time a new keyboard input event
        /// is about to be posted into a thread input queue.
        /// </summary>
        /// <remarks>
        /// If <code>nCode</code> is less than zero, the hook procedure must
        /// return the value returned by <c>CallNextHookEx</c>. If <code>nCode</code>
        /// is greater than or equal to zero, it is highly recommended that you call
        /// <c>CallNextHookEx</c> and return the value it returns. If the hook procedure
        /// does not call <c>CallNextHookEx</c>, the return value should be zero. See
        /// <a href="see https://docs.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-hookproc#return-value">
        /// HOOKPROC return value</a> for more details.
        /// </remarks>
        internal override HookProc Callback => (code, wParam, lParam) =>
        {
            if (code < 0)
            {
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }

            if (ShouldIgnoreApplicationFocus || Application.HasFocus())
            {
                var keyboardData =
                    Marshal.PtrToStructure<LowLevelKeyboardHookStruct>(lParam);

                var keyboardMessage =
                    (KeyboardMessage)wParam;

                OnKeyboardHookCalled(new KeyboardEventArgs
                {
                    VirtualKeyCode = keyboardData.vkCode,
                    KeyboardMessage = keyboardMessage
                });
            }

            return ShouldPreventNextHook ? 0 : CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        };
    }
}