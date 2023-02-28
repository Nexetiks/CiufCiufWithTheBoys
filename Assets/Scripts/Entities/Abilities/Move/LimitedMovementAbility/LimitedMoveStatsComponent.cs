using System;
using Entities.Components;
using UnityEngine;

namespace Entities.Abilities.LimitedMove
{
    [Serializable]
    public class LimitedMoveStatsComponent : BaseComponent
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private float maxAngle;

        public Stat Speed { get; private set; }
        public Stat MaxAngle { get; private set; }

        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            Speed = new Stat(speed);
            MaxAngle = new Stat(maxAngle);
        }

        public override object Clone()
        {
            LimitedMoveStatsComponent limitedMoveStatsComponent = (LimitedMoveStatsComponent)base.Clone();
            limitedMoveStatsComponent.Speed = new Stat(Speed.BaseValue);
            limitedMoveStatsComponent.MaxAngle = new Stat(MaxAngle.BaseValue);

            return limitedMoveStatsComponent;
        }
    }
}