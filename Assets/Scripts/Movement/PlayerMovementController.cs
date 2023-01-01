using System;
using Entities.Player;
using UnityEngine;

namespace Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private int dirPrev;
        private int lastDir;

        private PlayerStatsModel statsModel;

        public bool CanMove { get; set; } = true;

        private void Awake ()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            statsModel = GetComponent<PlayerContext>().PlayerStatsModel;
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
                
                rb.AddTorque(dir * statsModel.RotationSpeed.Value, ForceMode2D.Impulse);
                rb.AddForce(transform.up * (statsModel.ForwardForce.Value * Mathf.Abs(dir)), ForceMode2D.Impulse);
            }
            
            if (rb.velocity.magnitude > statsModel.MaxSpeed.Value)
            {
                rb.velocity = statsModel.MaxSpeed.Value * (rb.velocity).normalized;
            }
            
            dirPrev = dir;
            
            if (dirPrev != 0)
            {
                lastDir = dirPrev;
            }
        }
    }
}