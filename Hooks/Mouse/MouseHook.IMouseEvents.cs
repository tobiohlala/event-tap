using System;

using EventTap.Events;

namespace EventTap.Hooks
{
    public partial class MouseHook
    {
        public event EventHandler<MouseEventArgs> MouseMoved;
        public event EventHandler<MouseEventArgs> LeftMouseButtonPressed;
        public event EventHandler<MouseEventArgs> LeftMouseButtonReleased;
        public event EventHandler<MouseEventArgs> RightMouseButtonPressed;
        public event EventHandler<MouseEventArgs> RightMouseButtonReleased;
        public event EventHandler<MouseEventArgs> MiddleMouseButtonPressed;
        public event EventHandler<MouseEventArgs> MiddleMouseButtonReleased;
        public event EventHandler<MouseEventArgs> VerticalMouseWheelUpScrolled;
        public event EventHandler<MouseEventArgs> VerticalMouseWheelDownScrolled;
        public event EventHandler<MouseEventArgs> HorizontalMouseWheelLeftScrolled;
        public event EventHandler<MouseEventArgs> HorizontalMouseWheelRightScrolled;

        private void OnMouseHookCalled(MouseEventArgs e)
        {
            // dispatch corresponding event based on windows mouse message
            switch (e.MouseMessage)
            {
                case MouseMessage.LeftButtonDown:

                    LeftMouseButtonPressed?.Invoke(this, e);
                    break;

                case MouseMessage.LeftButtonUp:

                    LeftMouseButtonReleased?.Invoke(this, e);
                    break;

                case MouseMessage.RightButtonDown:

                    RightMouseButtonPressed?.Invoke(this, e);
                    break;

                case MouseMessage.RightButtonUp:

                    RightMouseButtonReleased?.Invoke(this, e);
                    break;

                case MouseMessage.MiddleButtonDown:

                    MiddleMouseButtonPressed?.Invoke(this, e);
                    break;

                case MouseMessage.MiddleButtonUp:

                    MiddleMouseButtonReleased?.Invoke(this, e);
                    break;

                case MouseMessage.VerticalMouseWheel:

                    if (e.MouseTransitionState == MouseTransitionState.MouseWheelUp)
                    {
                        VerticalMouseWheelUpScrolled?.Invoke(this, e);
                    }
                    else
                    {
                        VerticalMouseWheelDownScrolled?.Invoke(this, e);
                    }
                    break;

                case MouseMessage.HorizontalMouseWheel:

                    if (e.MouseTransitionState == MouseTransitionState.MouseWheelRight)
                    {
                        HorizontalMouseWheelRightScrolled?.Invoke(this, e);
                    }
                    else
                    {
                        HorizontalMouseWheelLeftScrolled?.Invoke(this, e);
                    }
                    break;

                default:

                    MouseMoved?.Invoke(this, e);
                    break;
            }
        }
    }
}
