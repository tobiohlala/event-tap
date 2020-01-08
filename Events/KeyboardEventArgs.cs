namespace EventTap.Events
{
    /// <summary>
    /// <c>KeyboardMessages</c> emitted by the system.
    /// </summary>
    public enum KeyboardMessage
    {
        KeyDown = 0x100,
        KeyUp = 0x101,
        SysKeyDown = 0x104,
        SysKeyUp = 0x105
    }

    /// <summary>
    /// Indicates the key transitioning occured with the event.
    /// </summary>
    public enum KeyboardTransitionState
    {
        KeyDown,
        KeyUp
    }

    /// <summary>
    /// Encapsulates keyboard event information.
    /// </summary>
    public class KeyboardEventArgs
    {
        /// <summary>
        /// The virtual key code identifier.
        /// </summary>
        public uint VirtualKeyCode { get; internal set; }

        /// <summary>
        /// The received <see cref="KeyboardMessage"/>.
        /// </summary>
        public KeyboardMessage KeyboardMessage { get; internal set; }

        /// <summary>
        /// The key transitioning occured.
        /// </summary>
        public KeyboardTransitionState TransitionState
        {
            get
            {
                switch (KeyboardMessage)
                {
                    case KeyboardMessage.KeyDown:
                    case KeyboardMessage.SysKeyDown:

                        return KeyboardTransitionState.KeyDown;

                    default:

                        return KeyboardTransitionState.KeyUp;
                }
            }
        }
    }
}