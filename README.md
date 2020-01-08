# event-tap

subscribe to system-raised events like mouse or keyboard input messages.

## example

```C#
var tap = new EventTap<IMouseEvents>(eventTapSource: new MouseHook
{
    // receive mouse events for all applications
    ShouldIgnoreApplicationFocus = true
});

tap.EventsOfInterest.LeftMouseButtonPressed += (object sender, MouseEventArgs e) =>
{
    // on left mouse button click
    ...
};

tap.Enable();
```

## wip

* [ ] keymaps / string representation of keys in `KeyboardEventArgs`
* [ ] support for different keyboard layouts
* [ ] raw input devices as alternative input event sources
* [ ] support for more events
