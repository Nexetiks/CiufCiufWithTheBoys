using Entities.Effects;
using UnityEngine;

namespace Entities.Abilities.DefaultMove
{
    public class DefaultMoveAbilityArgs : DefaultAbilityArgs
    {
        public Vector2 PositionToMoveAt { get; private set; }

        public Rigidbody2D Rb { get; private set; }

        public DefaultMoveAbilityArgs(Vector2 positionToMoveAt, Rigidbody2D rb)
        {
            PositionToMoveAt = positionToMoveAt;
            Rb = rb;
        }
    }
}