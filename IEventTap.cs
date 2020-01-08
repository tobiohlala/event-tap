using EventTap.Events;

namespace EventTap
{
    /// <summary>
    /// An <c>EventTap</c> monitors certain system events or messages you can
    /// subscribe to.
    /// </summary>
    /// <typeparam name="E">The events of interest to monitor.</typeparam>
    public interface IEventTap<E> where E : IEvents
    {
        /// <summary>
        /// An <see cref="IEventTapSource{E}"/> tapping the system and providing
        /// access to all the <see cref="EventsOfInterest"/>.
        /// </summary>
        IEventTapSource<E> Source { set; }

        /// <summary>
        /// Enable subscription to all the forwarded events provided by
        /// the <see cref="Source"/>.
        /// </summary>
        E EventsOfInterest { get; }

        /// <summary>
        /// Whether the <c>EventTap</c> is currently enabled.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Enables the <c>Event Tap</c>.
        /// </summary>
        void Enable();

        /// <summary>
        /// Disables the <c>Event Tap</c>.
        /// </summary>
        void Disable();
    }
}
