using System;
using System.Runtime.InteropServices;

using EventTap.Events;
using EventTap.Utils;

namespace EventTap.Hooks
{
    /// <summary>
    /// Low level hook enabling monitoring of mouse input events about to be posted
    /// in a thread input queue.<br/>
    /// See
    /// <a href="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644986(v%3Dvs.85)#remarks">
    /// LowLevelMouseProc</a> for more details.
    /// </summary>
    public partial class MouseHook : Hook, IMouseEvents, IEventTapSource<IMouseEvents>
    {
        /// <summary>
        /// Whether to respect natural scrolling, e.g on touchpads.<br/>
        /// If set to <c>true</c>, <see cref="MouseTransitionState.MouseWheelDown"/> and
        /// <see cref="MouseTransitionState.MouseWheelUp"/> events are inverted.
        /// </summary>
        public bool HasScrollingInverted { get; set; } = false;

        public override bool ShouldIgnoreApplicationFocus { get; set; } = false;

        public override bool ShouldPreventNextHook { get; set; } = false;

        internal override HookType Type => HookType.LowLevelMouseHook;

        /// <summary>
        /// Processes the received windows mouse message and raises the
        /// corresponding <see cref="IMouseEvents"/>.<br/>
        /// The system calls this function every time a new mouse input event
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
        internal override HookProc Callback => (nCode, wParam, lParam) =>
        {
            if (nCode < 0)
            {
                return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
            }

            if (ShouldIgnoreApplicationFocus || Application.HasFocus())
            {
                var mouseData =
                    Marshal.PtrToStructure<LowLevelMouseHookStruct>(lParam);

                var mouseMessage =
                    (MouseMessage)wParam;

                int mouseWheelDelta = 0;

                if (mouseMessage == MouseMessage.VerticalMouseWheel ||
                    mouseMessage == MouseMessage.HorizontalMouseWheel)
                {
                    // If the message is WM_MOUSEWHEEL the high-order word of this member is the wheel delta.
                    // A positive value indicates that the wheel was rotated forward, away from the user; a
                    // negative value indicates that the wheel was rotated backward, toward the user.
                    // see https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msllhookstruct#members

                    mouseWheelDelta = (short)(mouseData.mouseData >> 16);
                    mouseWheelDelta = HasScrollingInverted ? -mouseWheelDelta : mouseWheelDelta;
                }

                OnMouseHookCalled(new MouseEventArgs
                {
                    MouseCoordinates = mouseData.pt,
                    MouseMessage = mouseMessage,
                    MouseWheelDelta = mouseWheelDelta
                });
            }

            return ShouldPreventNextHook ? 0 : CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        };
    }
}