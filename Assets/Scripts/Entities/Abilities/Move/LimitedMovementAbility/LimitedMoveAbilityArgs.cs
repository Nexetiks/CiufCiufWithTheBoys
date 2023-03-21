using Entities.Effects;
using UnityEngine;

namespace Entities.Abilities.LimitedMove
{
    public class LimitedMoveAbilityArgs : DefaultAbilityArgs
    {
        public Vector2 Direction { get; private set; }

        public LimitedMoveAbilityArgs(Vector2 direction)
        {
            Direction = direction;
        }
    }
}