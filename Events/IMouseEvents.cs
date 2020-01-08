using System;

namespace EventTap.Events
{
    /// <summary>
    /// <c>Events of interest</c> for mouse input.
    /// </summary>
    public interface IMouseEvents : IEvents
    {
        /// <summary>
        /// Triggered when the mouse is moved.<br/>
        /// </summary>
        event EventHandler<MouseEventArgs> MouseMoved;

        /// <summary>
        /// Triggered when the left mouse button is pressed.
        /// </summary>
        event EventHandler<MouseEventArgs> LeftMouseButtonPressed;

        /// <summary>
        /// Triggered when the left mouse button is released.
        /// </summary>
        event EventHandler<MouseEventArgs> LeftMouseButtonReleased;

        /// <summary>
        /// Triggered when the right mouse button is pressed.
        /// </summary>
        event EventHandler<MouseEventArgs> RightMouseButtonPressed;

        /// <summary>
        /// Triggered when the right mouse button is released.
        /// </summary>
        event EventHandler<MouseEventArgs> RightMouseButtonReleased;

        /// <summary>
        /// Triggered when the middle mouse button is pressed.
        /// </summary>
        event EventHandler<MouseEventArgs> MiddleMouseButtonPressed;

        /// <summary>
        /// Triggered when the middle mouse button is released.
        /// </summary>
        event EventHandler<MouseEventArgs> MiddleMouseButtonReleased;

        /// <summary>
        /// Triggered when the mouse wheel is vertically scrolled upwards.
        /// </summary>
        event EventHandler<MouseEventArgs> VerticalMouseWheelUpScrolled;

        /// <summary>
        /// Triggered when the mouse wheel is vertically scrolled downwards.
        /// </summary>
        event EventHandler<MouseEventArgs> VerticalMouseWheelDownScrolled;

        /// <summary>
        /// Triggered when the mouse wheel is horizontally scrolled left.
        /// </summary>
        event EventHandler<MouseEventArgs> HorizontalMouseWheelLeftScrolled;

        /// <summary>
        /// Triggered when the mouse wheel is horizontally scrolled right.
        /// </summary>
        event EventHandler<MouseEventArgs> HorizontalMouseWheelRightScrolled;
    }
}
