using EarthEater.RailwaySystem.WagonEffects;
using Entities.Components;
using UnityEngine;

namespace EarthEater.RailwaySystem
{
    [System.Serializable]
    public class WagonComponent : BaseComponent
    {
        [field: SerializeField] public float Weight { get; private set; }

        [field: SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName), ReorderableList]
        public WagonEffect[] WagonEffects { get; private set; }

        public WagonComponent PreviousWagon { get; set; }
        public WagonComponent NextWagon { get; set; }

        public void OnAttached()
        {
            if (WagonEffects != null)
                foreach (WagonEffect wagonEffect in WagonEffects)
                {
                    wagonEffect.OnAttach(MyEntity);
                }
        }

        public void OnDetach()
        {
            if (WagonEffects != null)
                foreach (WagonEffect wagonEffect in WagonEffects)
                {
                    wagonEffect.OnDetach(MyEntity);
                }
        }

        public override object Clone()
        {
            WagonComponent clone = (WagonComponent) base.Clone();
            clone.WagonEffects = WagonEffects;

            return clone;
        }
    }
}