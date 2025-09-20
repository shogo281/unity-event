using System;
using System.Collections.Generic;
using Kuro.Addressables;
using Kuro.Common;
using Kuro.Editor.Runtime;
using UnityEngine;

namespace Kuro.Event
{
    public abstract class EventObjectBaseInternal<T> : ScriptableObject, IAddressableSettings
    {
        private readonly List<IEventListener<T>> m_Listeners = new();

        [ShowInInspector]
        protected int ListenerCount => m_Listeners.Count;

        public virtual IDisposable RegisterWithState<TState>(TState state, Action<T, TState> action)
        {
            var listener = new EventListenerWithState<T, TState>(state, action);
            m_Listeners.Add(listener);
            return Disposable.Create((self: this, listener), static t => t.self.m_Listeners.Remove(t.listener));
        }

        public virtual IDisposable Register(Action<T> action)
        {
            var listener = new EventListener<T>(action);
            m_Listeners.Add(listener);
            return Disposable.Create((self: this, listener), static t => t.self.m_Listeners.Remove(t.listener));
        }

        protected void InvokeInternal(T arg)
        {
            foreach (var listener in m_Listeners)
            {
                listener?.Invoke(arg);
            }
        }
    }
}