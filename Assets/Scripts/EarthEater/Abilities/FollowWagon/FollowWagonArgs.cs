using EarthEater.RailwaySystem;
using Entities;
using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.FollowWagon
{
    public class FollowWagonArgs : DefaultAbilityEffectArgs
    {
        public WagonComponent WagonComponent{ get; set; }
        public Rigidbody2D Rb { get; private set; }

        public FollowWagonArgs(Entity abilityOwner, WagonComponent wagonComponent, Rigidbody2D rb) : base(abilityOwner)
        {
            this.WagonComponent = wagonComponent;
            this.Rb = rb;
        }
    }
}
