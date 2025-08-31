using UnityEngine;

namespace Kuro.Event
{
    public readonly struct Unit
    {
        public static readonly Unit Instance = new Unit();
    }

    /// <summary>
    /// 引数なしイベント。
    /// </summary>
    [CreateAssetMenu(fileName = "evt.asset", menuName = "Kuro/Events/Event")]
    public class EventObject : EventObjectBase<Unit>
    {
        public void Invoke()
        {
            base.Invoke(Unit.Instance);
        }

        private new void Invoke(Unit unit)
        {
        }
    }
}