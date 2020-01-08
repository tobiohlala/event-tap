using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace EventTap.Utils
{
    /// <summary>
    /// Low level hooks like <see cref="EventTap.Hooks.MouseHook"/> or <see cref="EventTap.Hooks.KeyboardHook"/>
    /// are called in the context of the thread that installed it. The call is made by sending a message to the
    /// thread that installed the hook. Therefore, the thread that installed the hook must have a message loop.<br/>
    /// See
    /// <a href="https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644986(v%3Dvs.85)#remarks">
    /// HOOKPROC</a> for more details.
    /// </summary>
    internal class MessageLoop
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            public int x;
            public int y;
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        private struct Msg
        {
            IntPtr hwnd;
            uint message;
            UIntPtr wParam;
            IntPtr lParam;
            int time;
            Point pt;
        }

        [DllImport("user32.dll")]
        private static extern bool PeekMessage(out Msg lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

        [DllImport("user32.dll")]
        private static extern bool TranslateMessage([In] ref Msg lpMsg);

        [DllImport("user32.dll")]
        private static extern IntPtr DispatchMessage([In] ref Msg lpmsg);

        private bool _isRunning;

        /// <summary>
        /// Start a message loop in the current thread.
        /// </summary>
        internal void Start()
        {
            if (!_isRunning)
            {
                _isRunning = true;
                RunMessageLoop();
            }
        }

        /// <summary>
        /// Stop the message loop.
        /// </summary>
        internal void Stop()
        {
            _isRunning = false;
        }

        private void RunMessageLoop()
        {
            while (_isRunning)
            {
                if (PeekMessage(out Msg msg, IntPtr.Zero, 0, 0, 1))
                {
                    TranslateMessage(ref msg);
                    DispatchMessage(ref msg);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
    }
}
