using EarthEater.Components;
using Entities;
using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.MoveEngine
{
    public class MoveEngineAbilityArgs : DefaultAbilityEffectArgs
    {
        public Rigidbody2D Rb { get; private set; }
        public EngineComponent EngineComponent { get; private set; }
        public Transform Transform { get; private set; }
        public int Dir { get; set; }

        public MoveEngineAbilityArgs(Entity abilityOwner, Rigidbody2D rb, EngineComponent engineComponent,
            Transform transform) : base(abilityOwner)
        {
            this.Rb = rb;
            this.EngineComponent = engineComponent;
            this.Transform = transform;
        }
    }
}