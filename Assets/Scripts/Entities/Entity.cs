using System;
using System.Collections.Generic;
using Entities.Components;
using Entities.Effects.Statuses;
using UnityEngine;

namespace Entities
{
    [Serializable]
    public class Entity
    {
        private Dictionary<Type, BaseComponent> components;
        public EntityStatusesHandler StatusesHandler { get; private set; }
        public GameObject GameObject { get; private set; }
        public Transform Transform => GameObject.transform;

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

        public bool TryGetInterface<T>(out T interfaceComponent)
        {
            interfaceComponent = default;
            
            foreach (BaseComponent baseComponent in components.Values)
            {
                if (baseComponent is T returnComponent)
                {
                    interfaceComponent = returnComponent;
                    return true;
                }
            }

            return false;
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
        
        public void LateUpdate()
        {
            foreach (BaseComponent component in components.Values)
            {
                component.LateUpdateComponent();
            }
        }
    }
}