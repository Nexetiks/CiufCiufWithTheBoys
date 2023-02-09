using System;
using Entities.Effects;

namespace Entities.Abilities.DefaultAttack
{
    [Serializable]
    public class DefaultAttackAbilityArgs : DefaultAbilityArgs
    {
        public float damage { get; private set; }

        public DefaultAttackAbilityArgs(float damage)
        {
            this.damage = damage;
        }
    }
}