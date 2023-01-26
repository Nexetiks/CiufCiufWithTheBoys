using System.Collections.Generic;
using EarthEater.Components;
using Entities;
using Entities.Systems;
using UnityEngine;

namespace EarthEater.Systems
{
    public class MoveEngineSystem : EntitySystem<MoveEngineSystem.MovingEngineSystemArgs>
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            foreach (KeyValuePair<Entity,MovingEngineSystemArgs> keyValuePair in entityToSystemArgsLut)
            {
                EngineComponent engineComponent = keyValuePair.Value.EngineComponent;
                Rigidbody2D rb = keyValuePair.Value.Rb;
                
                rb.velocity = rb.transform.up * rb.velocity.magnitude;

                if (rb.velocity.magnitude > engineComponent.MaxSpeed.Value)
                {
                    rb.velocity = engineComponent.MaxSpeed.Value * (rb.velocity).normalized;
                }
            }
        }

        protected override void Update()
        {
            base.Update();
            foreach (KeyValuePair<Entity,MovingEngineSystemArgs> keyValuePair in entityToSystemArgsLut)
            {
                EngineComponent engineComponent = keyValuePair.Value.EngineComponent;
                Rigidbody2D rb = keyValuePair.Value.Rb;
                InputComponent inputComponent = keyValuePair.Value.InputComponent;

                if (inputComponent.HorizontalInput != 0 && engineComponent.LastDir != inputComponent.HorizontalInput)
                {
                    rb.angularVelocity = 0;
                }

                rb.AddTorque(inputComponent.HorizontalInput * engineComponent.RotationSpeed.Value, ForceMode2D.Impulse);
                rb.AddForce(rb.transform.up * ( Mathf.Max(engineComponent.ForwardForce.Value, 0) * Mathf.Abs(inputComponent.HorizontalInput)),
                    ForceMode2D.Impulse);

                if (inputComponent.HorizontalInput != 0)
                {
                    engineComponent.LastDir = inputComponent.HorizontalInput;
                }
            }
        }
        
        public struct MovingEngineSystemArgs
        {
            public Rigidbody2D Rb { get; private set; }
            public EngineComponent EngineComponent { get; private set; }
            public InputComponent InputComponent { get; private set; }
        }
    }
}
