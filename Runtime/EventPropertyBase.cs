using System;

namespace Kuro.Event
{
    public abstract class EventPropertyBase<T> : EventObjectBaseInternal<T>
        where T : IEquatable<T>
    {
        private T m_Value;

        public T Value
        {
            get { return m_Value; }
            set
            {
                if ((m_Value == null && value != null) || m_Value?.Equals(value) == false)
                {
                    InvokeInternal(value);
                    m_Value = value;
                }
            }
        }

        public override IDisposable RegisterWithState<TState>(TState state, Action<T, TState> action)
        {
            var disposable = base.RegisterWithState(state, action);

            // 登録時に一回実行する。
            action(Value, state);
            return disposable;
        }

        public override IDisposable Register(Action<T> action)
        {
            var disposable = base.Register(action);

            // 登録時に一回実行する。
            action(Value);
            return disposable;
        }
    }
}