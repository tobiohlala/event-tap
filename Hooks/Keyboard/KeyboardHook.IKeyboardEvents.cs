using System;

using EventTap.Events;

namespace EventTap.Hooks
{
    public partial class KeyboardHook
    {
        public event EventHandler<KeyboardEventArgs> KeyPressed;
        public event EventHandler<KeyboardEventArgs> KeyReleased;

        private void OnKeyboardHookCalled(KeyboardEventArgs e)
        {
            // dispatch corresponding event based on windows mouse message
            if (e.TransitionState == KeyboardTransitionState.KeyDown)
            {
                KeyPressed?.Invoke(this, e);
            }
            else
            {
                KeyReleased?.Invoke(this, e);
            }
        }
    }
}