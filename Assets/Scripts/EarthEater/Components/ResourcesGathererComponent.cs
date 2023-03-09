using Entities;
using Entities.Components;
using UnityEngine;

namespace EarthEater.Components
{
    public class ResourcesGathererComponent : BaseComponent
    {
        [SerializeField]
        private LayerMask resourceLayerMask;
        
        [SerializeField]
        private float detectionRadius=3f;

        [SerializeField]
        private float pickupRadius = .2f;

        [SerializeField]
        private float pullForce = 15f;
        public override void UpdateComponent()
        {
            base.UpdateComponent();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(MyEntity.Transform.position, detectionRadius, resourceLayerMask);
            if (colliders.Length <= 0) return;
            
            foreach (Collider2D collider in colliders)
            {
                if (!CheckIfIsPickable(collider, out IAmPickable pickable))
                {
                    continue;
                }

                Vector2 distanceVector = (Vector2) Transform.position - (Vector2)pickable.Rb.transform.position;
                
                if (TryPickup(distanceVector, pickable))
                {
                    continue;
                }
                
                ApplyPullForce(pickable, distanceVector);
            }
        }

        private static bool CheckIfIsPickable(Collider2D collider, out IAmPickable pickable)
        {
            pickable = null;
            
            if (!collider.TryGetComponent(out EntityContext context)) return false;
            if (!context.Entity.TryGetInterface(out pickable)) return false;
            
            return true;
        }

        private bool TryPickup(Vector2 distanceVector, IAmPickable pickable)
        {
            if (distanceVector.magnitude > pickupRadius) return false;
            
            pickable.OnPickUp();
            return true;
        }

        private void ApplyPullForce(IAmPickable pickable, Vector2 distanceVector)
        {
            Rigidbody2D rb = pickable.Rb;
            rb.AddForce(distanceVector.normalized * Time.deltaTime * pullForce);
        }
    }
}
