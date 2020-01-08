namespace EventTap.Hooks
{
    /// <summary>
    /// A hook is a mechanism by which an application can intercept
    /// events such as mouse actions and keystrokes.<br/>
    /// See 
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/winmsg/about-hooks">
    /// About Hooks</a> for more details.
    /// </summary>
    public interface IHook
    {
        /// <summary>
        /// Whether the hook should ignore application focus and raise
        /// events for all applications in the current desktop.
        /// </summary>
        bool ShouldIgnoreApplicationFocus { get; set; }

        /// <summary>
        /// Whether the hook should block execution of other hooks in
        /// the hook chain. By default this should be set to <c>false</c>.<br/>
        /// See
        /// <a href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex#remarks">
        /// CallNextHookEx</a> for more details.
        /// </summary>
        bool ShouldPreventNextHook { get; set; }

        /// <summary>
        /// Whether the hook is active and currently installed into the hook chain.
        /// </summary>
        bool IsInstalled { get; }

        /// <summary>
        /// Activates the hook by installing the hook procedure into the hook chain.
        /// </summary>
        void Install();

        /// <summary>
        /// Removes the hook by uninstalling the hook procedure from the hook chain.
        /// </summary>
        void Uninstall();
    }
}
