using Entities;
using Entities.Components;
using UnityEngine;

namespace EarthEater.Components
{
    public class PickableResourceComponent : BaseComponent, IAmPickable
    {
        public Rigidbody2D Rb { get; private set; }
        
        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            Rb = myEntity.GameObject.GetComponent<Rigidbody2D>();
        }

        public void OnPickUp()
        {
            GameObject.Destroy(MyEntity.GameObject);
        }
    }
}
