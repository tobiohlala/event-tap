using EventTap.Events;

namespace EventTap
{
    /// <summary>
    /// A registered <c>EventTapSource</c> taps the system and forwards certain
    /// events providing access to all the <see cref="EventsOfInterest"/>.
    /// </summary>
    /// <typeparam name="E">The events of interest to forward.</typeparam>
    public interface IEventTapSource<E> where E : IEvents
    {
        /// <summary>
        /// All the forwarded events.
        /// </summary>
        E EventsOfInterest { get; }

        /// <summary>
        /// Cause the <c>EventTapSource</c> to start tapping the system for events.
        /// </summary>
        /// <returns>Whether the <c>EventTapSource</c> could be started successfully.</returns>
        bool Tap();

        /// <summary>
        /// Cause the <c>EventTapSource</c> to stop tapping the system for events.
        /// </summary>
        /// <returns>Whether the <c>EventTapSource</c> could be stopped successfully.</returns>
        bool Untap();
    }
}
