using System.Numerics;
using Entities.Effects;

namespace Entities.Abilities.LimitedMove
{
    public class LimitedMoveAbilityArgs : DefaultAbilityArgs
    {
        public Vector3 Direction { get; private set; }

        public LimitedMoveAbilityArgs(Vector3 direction)
        {
            Direction = direction;
        }
    }
}