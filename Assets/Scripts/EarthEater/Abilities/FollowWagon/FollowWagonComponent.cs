using EarthEater.RailwaySystem;
using Entities;
using Entities.Abilities;
using Entities.Components;
using Entities.Effects;
using UnityEngine;

namespace EarthEater.Abilities.FollowWagon
{
    public class FollowWagonComponent : BaseComponent
    {
        private WagonComponent wagonComponent;
        private Rigidbody2D rb;
        public FollowWagonComponent()
        {
        }

        public override void FixedUpdateComponent()
        {
            base.FixedUpdateComponent();
            if(wagonComponent.NextWagon == null) return;

            Transform target = wagonComponent.NextWagon.MyEntity.GameObject.transform;
            Vector2 forwardDir = (Vector2)target.position - rb.position;
            rb.transform.up = forwardDir.normalized;
            rb.MovePosition((Vector2)target.position - forwardDir.normalized*1.75f);
        }

        public override void Initialize(Entity abilityOwner)
        {
            base.Initialize(abilityOwner);
            rb = abilityOwner.GameObject.GetComponent<Rigidbody2D>();
            wagonComponent = abilityOwner.GetComponent<WagonComponent>();
        }
    }
}