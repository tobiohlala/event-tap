using System;

namespace EventTap.Events
{
    /// <summary>
    /// <c>Events of interest</c> for keyboard input.
    /// </summary>
    public interface IKeyboardEvents : IEvents
    {
        /// <summary>
        /// Triggered when a key is pressed.<br/>
        /// </summary>
        event EventHandler<KeyboardEventArgs> KeyPressed;

        /// <summary>
        /// Triggered when a key is released.<br/>
        /// </summary>
        event EventHandler<KeyboardEventArgs> KeyReleased;
    }
}
