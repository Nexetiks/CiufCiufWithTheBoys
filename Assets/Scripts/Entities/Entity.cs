using System;
using System.Collections.Generic;
using Entities.Components;
using Entities.Effects.Statuses;
using UnityEngine;

namespace Entities
{
    public class Entity
    {
        private Dictionary<Type, BaseComponent> components;
        public EntityStatusesHandler StatusesHandler { get; private set; }
        public GameObject GameObject { get; private set; }

        public Entity(EntityDefaultData defaultData, GameObject gameObject)
        {
            StatusesHandler = new EntityStatusesHandler(this);
            components = new Dictionary<Type, BaseComponent>();
            GameObject = gameObject;

            foreach (BaseComponent component in defaultData.Components)
            {
                AddComponent((BaseComponent)component.Clone());
            }

            foreach (BaseComponent component in components.Values)
            {
                component.Initialize(this);
            }
        }

        public bool TryGetComponent<T>(out T component) where T : BaseComponent
        {
            component = null;

            if (components.TryGetValue(typeof(T), out BaseComponent returnComponent))
            {
                component = (T)returnComponent;
            }

            return component != null;
        }

        public T GetComponent<T>() where T : BaseComponent
        {
            return (T)components[typeof(T)];
        }

        public void AddComponent(BaseComponent component)
        {
            components.Add(component.GetType(), component);
        }

        public void Update()
        {
            StatusesHandler.UpdateStatuses();
            foreach (BaseComponent component in components.Values)
            {
                component.UpdateComponent();
            }
        }

        public void FixedUpdate()
        {
            foreach (BaseComponent component in components.Values)
            {
                component.FixedUpdateComponent();
            }
        }
    }
}