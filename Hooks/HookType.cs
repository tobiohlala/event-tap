namespace EventTap.Hooks
{
    /// <summary>
    /// Different types of hooks.<br/>
    /// See
    /// <a href="https://docs.microsoft.com/en-us/windows/win32/winmsg/about-hooks#hook-types">
    /// Hook Types</a> for more details.
    /// </summary>
    internal enum HookType : int
    {
        // WH_KEYBOARD_LL
        LowLevelKeyboardHook = 13,
        // WH_MOUSE_LL
        LowLevelMouseHook = 14
    }
}