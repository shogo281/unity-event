using System;
using System.Collections.Generic;
using Kuro.Addressables;
using Kuro.Common;
using Kuro.Editor.Runtime;
using UnityEngine;

namespace Kuro.Event
{
    public abstract class EventObjectBase<T> : EventObjectBaseInternal<T>
    {
        public void Invoke(T arg)
        {
            InvokeInternal(arg);
        }
    }
}