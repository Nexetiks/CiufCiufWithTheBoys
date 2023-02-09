using Entities.Components;
using UnityEngine;

namespace Entities.Abilities.DefaultAttack
{
    public class DefaultAttackStatsComponent : BaseComponent
    {
        [field: SerializeField]
        public Stat Cooldown { get; private set; }

        public override object Clone()
        {
            DefaultAttackStatsComponent attckComponent = (DefaultAttackStatsComponent)base.Clone();
            attckComponent.Cooldown = new Stat(Cooldown.BaseValue);

            return attckComponent;
        }
    }
}