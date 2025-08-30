using System;

namespace Kuro.Event
{
    internal interface IEventListener<in T>
    {
        void Invoke(T arg);
    }

    internal class EventListener<T> : IEventListener<T>
    {
        private readonly Action<T> m_Action;

        public EventListener(Action<T> action)
        {
            m_Action = action;
        }

        void IEventListener<T>.Invoke(T arg)
        {
            m_Action?.Invoke(arg);
        }
    }

    internal class EventListenerWithState<T, TState> : IEventListener<T>
    {
        private readonly Action<T, TState> m_Action;
        private readonly TState m_State;

        public EventListenerWithState(TState state, Action<T, TState> action)
        {
            m_State = state;
            m_Action = action;
        }

        void IEventListener<T>.Invoke(T arg)
        {
            m_Action?.Invoke(arg, m_State);
        }
    }
}