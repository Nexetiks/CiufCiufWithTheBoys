using Entities;
using Entities.Components;
using UnityEngine;

namespace EarthEater.Components
{
    public class EngineComponent : BaseComponent
    {
        [SerializeField] private float initialMaxSpeed;
        [SerializeField] private float initialForwardForce;
        [SerializeField] private float initialRotationSpeed;
        
        public Stat MaxSpeed { get; private set; }
        public Stat ForwardForce { get; private set; }
        public Stat RotationSpeed { get; private set; }

        public int LastDir { get; set; } = 0;

        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            MaxSpeed = new Stat(initialMaxSpeed);
            ForwardForce = new Stat(initialForwardForce);
            RotationSpeed = new Stat(initialRotationSpeed);
        }
    }
}
