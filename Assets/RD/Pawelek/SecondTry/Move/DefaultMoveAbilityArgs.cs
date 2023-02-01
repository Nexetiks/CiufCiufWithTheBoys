using Entities.Effects;
using UnityEngine;

public class DefaultMoveAbilityArgs : DefaultAbilityArgs
{
    public Vector2 PositionToMoveAt { get; private set; }

    public DefaultMoveAbilityArgs(Vector2 positionToMoveAt)
    {
        PositionToMoveAt = positionToMoveAt;
    }
}