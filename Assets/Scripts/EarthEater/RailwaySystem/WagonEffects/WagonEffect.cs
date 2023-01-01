using Entities;

namespace EarthEater.RailwaySystem.WagonEffects
{
    public abstract class WagonEffect
    {
        public WagonComponent MyWagonComponent { get; private set; }
        public abstract void OnAttach(Entity entity);
        public abstract void OnDetach(Entity entity);

        public virtual void Initialize(WagonComponent myWagonComponent)
        {
            this.MyWagonComponent = myWagonComponent;
        }
    }
}
