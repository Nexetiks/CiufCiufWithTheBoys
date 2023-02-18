using Entities.Components;
using UnityEngine;

namespace Entities.Abilities.DefaultAttack
{
    public class DefaultAttackStatsComponent : BaseComponent
    {
        [SerializeField]
        private float cooldown;

        public Stat Cooldown { get; private set; }

        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            Cooldown = new Stat(cooldown);
        }

        public override object Clone()
        {
            DefaultAttackStatsComponent attckComponent = (DefaultAttackStatsComponent)base.Clone();
            attckComponent.Cooldown = new Stat(Cooldown.BaseValue);

            return attckComponent;
        }
    }
}