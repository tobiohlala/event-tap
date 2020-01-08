using System.Threading;

using EventTap.Events;
using EventTap.Utils;

namespace EventTap.Hooks
{
    public partial class MouseHook
    {
        private readonly MessageLoop _messageLoop = new MessageLoop();

        public IMouseEvents EventsOfInterest => this;

        public bool Tap()
        {
            new Thread(() =>
            {
                // This hook is called in the context of the thread that installed it. As the
                // call is made by sending a message to the thread that installed the hook,
                // the thread that installed the hook must have a message loop. To ensure a
                // message loop is present for the hook to work even if the hook is installed
                // in the thread context of a console application having no message loop, hook
                // installation will be done in a separate thread with a separate message loop.
                // See https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644986(v%3Dvs.85)#remarks

                Install();

                _messageLoop.Start();

            }).Start();

            return IsInstalled;
        }

        public bool Untap()
        {
            _messageLoop.Stop();

            Uninstall();

            return !IsInstalled;
        }
    }
}
