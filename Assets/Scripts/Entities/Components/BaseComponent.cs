using System;

namespace Entities.Components
{
    [System.Serializable]
    public abstract class BaseComponent: ICloneable
    {
        public Entity MyEntity { get; private set; }

        public virtual void UpdateComponent()
        {
        }

        public virtual void FixedUpdateComponent()
        {
        }
        
        public virtual void Initialize(Entity myEntity)
        {
            MyEntity = myEntity;
        }

        public virtual object Clone()
        {
            BaseComponent copy = (BaseComponent)this.MemberwiseClone();
            return copy;
        }
    }
}
