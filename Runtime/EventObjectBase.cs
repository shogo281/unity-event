using System;
using System.Collections.Generic;
using Kuro.Common;
using UnityEngine;

namespace Kuro.Event
{
    public abstract class EventObjectBase<T> : ScriptableObject
    {
        private readonly List<IEventListener<T>> m_Listeners = new();

        public IDisposable RegisterWithState<TState>(TState state, Action<T, TState> action)
        {
            var listener = new EventListenerWithState<T, TState>(state, action);
            m_Listeners.Add(listener);
            return Disposable.Create((self: this, listener), static t => t.self.m_Listeners.Remove(t.listener));
        }

        public IDisposable Register(Action<T> action)
        {
            var listener = new EventListener<T>(action);
            m_Listeners.Add(listener);
            return Disposable.Create((self: this, listener), static t => t.self.m_Listeners.Remove(t.listener));
        }

        public void Invoke(T arg)
        {
            foreach (var listener in m_Listeners)
            {
                listener?.Invoke(arg);
            }
        }
    }
}