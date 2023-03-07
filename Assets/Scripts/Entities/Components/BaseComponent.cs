using System;
using UnityEngine;

namespace Entities.Components
{
    [Serializable]
    public abstract class BaseComponent : ICloneable
    {
        public Entity MyEntity { get; private set; }
        
        public Transform Transform => MyEntity.Transform;

        public virtual void UpdateComponent()
        {
        }

        public virtual void LateUpdateComponent()
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