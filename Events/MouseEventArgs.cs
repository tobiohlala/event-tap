using System.Runtime.InteropServices;

namespace EventTap.Events
{
    /// <summary>
    /// <c>MouseMessages</c> emitted by the system.
    /// </summary>
    public enum MouseMessage : int
    {
        MouseMove = 0x200,
        LeftButtonDown = 0x201,
        LeftButtonUp = 0x202,
        RightButtonDown = 0x204,
        RightButtonUp = 0x205,
        MiddleButtonDown = 0x207,
        MiddleButtonUp = 0x208,
        VerticalMouseWheel = 0x20a,
        HorizontalMouseWheel = 0x20e
    }

    /// <summary>
    /// Indicates the mouse transitioning occured with the event.
    /// </summary>
    public enum MouseTransitionState
    {
        MouseMove,
        MouseButtonDown,
        MouseButtonUp,
        MouseWheelUp,
        MouseWheelDown,
        MouseWheelLeft,
        MouseWheelRight
    }

    /// <summary>
    /// Defines the X- and Y- coordinates of a point on the monitor.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseCoordinates
    {
        public int X;
        public int Y;
    }

    /// <summary>
    /// Encapsulates mouse event information.
    /// </summary>
    public class MouseEventArgs
    {
        /// <summary>
        /// The location of the mouse on the monitor.
        /// </summary>
        public MouseCoordinates MouseCoordinates { get; internal set; }

        /// <summary>
        /// The received <see cref="MouseMessage"/>.
        /// </summary>
        public MouseMessage MouseMessage { get; internal set; }

        /// <summary>
        /// The mouse transitioning occured.
        /// </summary>
        public MouseTransitionState MouseTransitionState
        {
            get
            {
                switch (MouseMessage)
                {
                    case MouseMessage.LeftButtonDown:
                    case MouseMessage.RightButtonDown:
                    case MouseMessage.MiddleButtonDown:

                        return MouseTransitionState.MouseButtonDown;

                    case MouseMessage.LeftButtonUp:
                    case MouseMessage.RightButtonUp:
                    case MouseMessage.MiddleButtonUp:

                        return MouseTransitionState.MouseButtonUp;

                    case MouseMessage.VerticalMouseWheel:

                        return MouseWheelDelta > 0 ?
                            MouseTransitionState.MouseWheelUp :
                            MouseTransitionState.MouseWheelDown;

                    case MouseMessage.HorizontalMouseWheel:

                        return MouseWheelDelta > 0 ?
                            MouseTransitionState.MouseWheelRight :
                            MouseTransitionState.MouseWheelLeft;

                    default:

                        return MouseTransitionState.MouseMove;
                }
            }
        }

        /// <summary>
        /// Contains the mouse wheel delta resembling scrolling speed.<br/>
        /// For <see cref="MouseMessage.VerticalMouseWheel"/> scroll events a positive value
        /// indicates that the wheel was rotated forward, away from the user;
        /// a negative value indicates that the wheel was rotated backward, toward the user.<br/>
        /// For <see cref="MouseMessage.HorizontalMouseWheel"/> scroll events a positive value
        /// indicates that the wheel was rotated to the right;
        /// a negative value indicates that the wheel was rotated to the left.<br/>
        /// For any other <see cref="MouseMessage"/> event the value is <c>0</c>.<br/>
        /// See
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msllhookstruct#members">
        /// MSLLHOOKSTRUCT</a> for more details.
        /// </summary>
        public int MouseWheelDelta { get; internal set; } = 0;
    }
}