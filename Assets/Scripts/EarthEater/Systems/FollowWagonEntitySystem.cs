using System.Collections.Generic;
using EarthEater.Abilities.FollowWagon;
using EarthEater.Components;
using EarthEater.RailwaySystem;
using Entities;
using Entities.Systems;
using UnityEngine;

namespace EarthEater.Systems
{
    public class FollowWagonSystem : EntitySystem<FollowWagonSystem.FollowWagonArgs>
    {
        protected override void FixedUpdate()
        {
            foreach (KeyValuePair<Entity,FollowWagonArgs> keyValuePair in entityToSystemArgsLut)
            {
                Entity entity = keyValuePair.Key;
                CanFollowWagonComponent canFollowWagonComponent = keyValuePair.Value.CanFollowWagonComponent;
                WagonComponent wagonComponent = keyValuePair.Value.WagonComponent;
                Rigidbody2D rb = keyValuePair.Value.Rb;
                
                if(wagonComponent.NextWagon == null) return;

                Transform target = wagonComponent.NextWagon.MyEntity.GameObject.transform;
                Vector2 forwardDir = (Vector2)target.position - rb.position;
                rb.transform.up = forwardDir.normalized;
                rb.MovePosition((Vector2)target.position - forwardDir.normalized);
            }
        }
        
        public struct FollowWagonArgs
        {
            public CanFollowWagonComponent CanFollowWagonComponent { get; private set; }
            public WagonComponent WagonComponent { get; private set; }
            public Rigidbody2D Rb { get; private set; }

            public FollowWagonArgs(CanFollowWagonComponent canFollowWagonComponent, WagonComponent wagonComponent,
                Rigidbody2D rb)
            {
                this.CanFollowWagonComponent = canFollowWagonComponent;
                this.WagonComponent = wagonComponent;
                this.Rb = rb;
            }
        }
    }
}
