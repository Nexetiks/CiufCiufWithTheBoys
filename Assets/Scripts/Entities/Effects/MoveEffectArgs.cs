using UnityEngine;

namespace Entities.Effects
{
    public abstract class MoveEffectArgs : DefaultAbilityEffectArgs
    {
        public Vector2 Direction { get; private set; }
        
        public MoveEffectArgs(Entity abilityOwner, Vector2 direction) : base(abilityOwner)
        {
            this.Direction = direction;
        }
    }
}
