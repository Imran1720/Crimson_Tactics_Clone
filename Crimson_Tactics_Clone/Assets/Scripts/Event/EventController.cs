using System;

namespace CrimsonTactics.Events
{
    public class EventController
    {
        public Action currentEvent;
        public void InvokeEvent() => currentEvent?.Invoke();
        public void AddEventListener(Action listener) => currentEvent += listener;
        public void RemoveEventListener(Action listener) => currentEvent -= listener;
    }

    public class EventController<T>
    {
        public Action<T> currentEvent;
        public void InvokeEvent(T value) => currentEvent?.Invoke(value);
        public void AddEventListener(Action<T> listener) => currentEvent += listener;
        public void RemoveEventListener(Action<T> listener) => currentEvent -= listener;
    }
}
