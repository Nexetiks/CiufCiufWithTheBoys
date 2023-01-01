using EarthEater.Components;
using Entities;
using UnityEngine;

namespace EarthEater.Player.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private int dirPrev;
        private int lastDir;

        private EngineComponent engineComponent;

        public bool CanMove { get; set; } = true;

        private void Awake ()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GetComponent<EntityContext>().Entity.TryGetComponent(out engineComponent);
        }

        void FixedUpdate ()
        {
            if (!CanMove)
            {
                rb.velocity = Vector3.zero;
                return;
            }

            int dir = -(int)Input.GetAxisRaw("Horizontal");
            rb.velocity = transform.up * rb.velocity.magnitude;
            
            if (dir != dirPrev)
            {
                if (dir != 0 && lastDir != dir) rb.angularVelocity = 0;
                
                rb.AddTorque(dir * engineComponent.RotationSpeed.Value, ForceMode2D.Impulse);
                rb.AddForce(transform.up * (engineComponent.ForwardForce.Value * Mathf.Abs(dir)), ForceMode2D.Impulse);
            }
            
            if (rb.velocity.magnitude > engineComponent.MaxSpeed.Value)
            {
                rb.velocity = engineComponent.MaxSpeed.Value * (rb.velocity).normalized;
            }
            
            dirPrev = dir;
            
            if (dirPrev != 0)
            {
                lastDir = dirPrev;
            }
        }
    }
}