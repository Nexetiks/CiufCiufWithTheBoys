using Entities;
using Entities.Effects;
using UnityEngine;

namespace Pawelek.Testing.ItWillBeChangeLater
{
    public class MoveUnitAbilityArgs : DefaultAbilityArgs
    {
        public Rigidbody2D Rb { get; private set; }
        public Transform Transform { get; private set; }
        public Vector2 NextTarget { get; set; }
        public float DistanceToAttackPlayer { get; private set; }
        public Transform player { get; private set; }
        public bool Chasing { get; set; } = false;

        public MoveUnitAbilityArgs(Entity abilityOwner, Rigidbody2D rb, Transform transform, Vector2 nextTarget, float distanceToAttackPlayer, Transform player)
        {
            Rb = rb;
            Transform = transform;
            NextTarget = nextTarget;
            DistanceToAttackPlayer = distanceToAttackPlayer;
            this.player = player;
        }
    }
}