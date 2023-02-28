using System;
using Entities.Components;
using UnityEngine;

namespace Entities.Abilities.DefaultMove

{
    [Serializable]
    public class DefaultMoveStatsComponent : BaseComponent
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
            DefaultMoveStatsComponent defaultMoveStatsComponent = (DefaultMoveStatsComponent)base.Clone();
            defaultMoveStatsComponent.Speed = new Stat(Speed.BaseValue);

            return defaultMoveStatsComponent;
        }
    }
}