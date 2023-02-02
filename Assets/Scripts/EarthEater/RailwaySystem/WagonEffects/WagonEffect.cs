using System;
using Entities;

namespace EarthEater.RailwaySystem.WagonEffects
{
    [Serializable]
    public abstract class WagonEffect : ICloneable
    {
        public WagonComponent MyWagonComponent { get; private set; }
        public abstract void OnAttach(Entity entity);
        public abstract void OnDetach(Entity entity);

        public virtual void Initialize(WagonComponent myWagonComponent)
        {
            this.MyWagonComponent = myWagonComponent;
        }

        public object Clone()
        {
            WagonEffect copy = (WagonEffect)this.MemberwiseClone();
            return copy;
        }
    }
}