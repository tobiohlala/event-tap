using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace EventTap.Hooks
{
    /// <summary>
    /// Base class managing hook installation and uninstallation.
    /// </summary>
    public abstract class Hook : IHook, IDisposable
    {
        /// <summary>
        /// Installs a <see cref="HookProc"/> into a hook chain.<br/>
        /// See
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa">
        /// SetWindowsHookEx</a> for more details.
        /// </summary>
        /// <param name="hookType"></param>
        /// <param name="lpfn"></param>
        /// <param name="hMod"></param>
        /// <param name="dwThreadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);
        
        private SafeHookHandle _handle;

        private GCHandle _pinnedDelegate;

        public bool IsInstalled => _handle != null;

        public abstract bool ShouldIgnoreApplicationFocus { get; set; }

        public abstract bool ShouldPreventNextHook { get; set; }

        /// <summary>
        /// The <see cref="HookType"/> to monitor system events for.
        /// </summary>
        internal abstract HookType Type { get; }

        /// <summary>
        /// The <see cref="HookProc"/> callback to install into a hook chain.
        /// </summary>
        internal abstract HookProc Callback { get; }

        /// <inheritdoc/>
        /// <exception cref="Win32Exception"/>
        public void Install()
        {
            if (IsInstalled)
            {
                return;
            }

            // prevent managed callback from being garbage collected
            _pinnedDelegate = GCHandle.Alloc(Callback);

            _handle = new SafeHookHandle(
                SetWindowsHookEx(Type, Callback, IntPtr.Zero, 0));

            if (_handle.IsInvalid)
            {
                throw new Win32Exception("SetWindowsHookEx error: " + Marshal.GetLastWin32Error());
            }
        }

        public void Uninstall()
        {
            if (!IsInstalled)
            {
                return;
            }

            _handle.Dispose();

            _handle = null;
            _pinnedDelegate.Free();
        }

        public void Dispose()
        {
            Uninstall();
        }
    }
}