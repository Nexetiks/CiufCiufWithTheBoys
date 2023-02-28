using Entities.Effects;
using UnityEngine;

namespace Entities.Abilities.DefaultMove
{
    public class DefaultMoveAbilityArgs : DefaultAbilityArgs
    {
        public Vector2 Direction { get; private set; }

        public DefaultMoveAbilityArgs(Vector2 direction)
        {
            Direction = direction;
        }
    }
}