using UnityEngine;

namespace Entities.Components
{
    [System.Serializable]
    public abstract class BaseComponent
    {
        public Entity MyEntity { get; private set; }

        public virtual void Initialize(Entity myEntity)
        {
            MyEntity = myEntity;
        }
    }
}
