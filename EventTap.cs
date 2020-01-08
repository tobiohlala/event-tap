using EventTap.Events;

namespace EventTap
{
    /// <summary>
    /// Generic implementation of an <c>EventTap</c> to monitor certain
    /// system events or messages you can subscribe to.<br/>
    /// See <see cref="IEventTap{E}"/>.
    /// </summary>
    /// <typeparam name="E">The events of interest to monitor.</typeparam>
    public class EventTap<E> : IEventTap<E> where E : IEvents
    {
        private IEventTapSource<E> _source;

        public IEventTapSource<E> Source
        {
            private get => _source;

            set
            {
                _source = value;
                EventsOfInterest = _source.EventsOfInterest;
            }
        }

        public E EventsOfInterest { get; private set; }

        public bool IsEnabled { get; private set; } = false;

        public EventTap(IEventTapSource<E> eventTapSource) => Source = eventTapSource;

        public void Enable()
        {
            IsEnabled = Source.Tap();
        }

        public void Disable()
        {
            IsEnabled = Source.Untap();
        }
    }
}
