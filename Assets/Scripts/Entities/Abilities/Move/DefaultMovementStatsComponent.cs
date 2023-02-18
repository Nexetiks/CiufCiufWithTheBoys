using Entities.Components;
using UnityEngine;

namespace Entities.Abilities.DefaultMove
{
    public class DefaultMovementStatsComponent : BaseComponent
    {
        [SerializeField]
        private float speed;

        public Stat Speed { get; private set; }

        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            Speed = new Stat(speed);
        }

        public override object Clone()
        {
            DefaultMovementStatsComponent wagonComponent = (DefaultMovementStatsComponent)base.Clone();
            wagonComponent.Speed = new Stat(Speed.BaseValue);

            return wagonComponent;
        }
    }
}