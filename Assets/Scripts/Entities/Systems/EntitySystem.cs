using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Entities.Systems
{
    public class EntitySystem<TSystemArgs>
    {
        protected Dictionary<Entity, TSystemArgs> entityToSystemArgsLut;

        public EntitySystem()
        {
            entityToSystemArgsLut = new Dictionary<Entity, TSystemArgs>();
        }

        public void Subscribe(Entity entity, TSystemArgs component)
        {
            entityToSystemArgsLut.Add(entity, component);
        }

        public void Unsubscribe(Entity entity)
        {
            entityToSystemArgsLut.Remove(entity);
        }

        protected virtual void Update()
        {
        }

        protected virtual void FixedUpdate()
        {
        }
    }
}