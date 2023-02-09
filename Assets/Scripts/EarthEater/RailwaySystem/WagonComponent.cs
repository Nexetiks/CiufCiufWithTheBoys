using System;
using EarthEater.RailwaySystem.WagonEffects;
using Entities;
using Entities.Components;
using UnityEngine;

namespace EarthEater.RailwaySystem
{
    [Serializable]
    public class WagonComponent : BaseComponent
    {
        [field: SerializeField]
        public float Weight { get; private set; }

        [field: SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName), ReorderableList]
        public WagonEffect[] WagonEffects { get; private set; }

        public WagonComponent PreviousWagon { get; set; }
        public WagonComponent NextWagon { get; set; }
        public Entity WagonHeadEntity { get; set; }

        public WagonComponent()
        {
            if (WagonEffects == null) return;

            foreach (WagonEffect effect in WagonEffects)
            {
                effect.Initialize(this);
            }
        }

        public void OnAttached()
        {
            if (WagonEffects == null) return;

            foreach (WagonEffect effect in WagonEffects)
            {
                effect.Initialize(this);
            }

            foreach (WagonEffect wagonEffect in WagonEffects)
            {
                wagonEffect.OnAttach(MyEntity);
            }
        }

        public void OnDetach()
        {
            if (WagonEffects == null) return;

            foreach (WagonEffect wagonEffect in WagonEffects)
            {
                wagonEffect.OnDetach(MyEntity);
            }
        }

        public override object Clone()
        {
            WagonComponent wagonComponent = (WagonComponent)base.Clone();
            wagonComponent.WagonEffects = new WagonEffect[WagonEffects.Length];

            for (int i = 0; i < WagonEffects.Length; i++)
            {
                wagonComponent.WagonEffects[i] = (WagonEffect)WagonEffects[i].Clone();
            }

            return wagonComponent;
        }
    }
}