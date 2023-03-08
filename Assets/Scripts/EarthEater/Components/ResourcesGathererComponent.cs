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
                if (!CheckIfIsPickable(collider, out PickableResourceComponent pickableResourceComponent))
                {
                    continue;
                }

                Vector2 distanceVector = (Vector2) Transform.position - (Vector2)pickableResourceComponent.Transform.position;
                
                if (TryPickup(distanceVector, pickableResourceComponent))
                {
                    continue;
                }
                
                ApplyPullForce(pickableResourceComponent, distanceVector);
            }
        }

        private static bool CheckIfIsPickable(Collider2D collider, out PickableResourceComponent pickableResourceComponent)
        {
            pickableResourceComponent = null;
            
            if (!collider.TryGetComponent(out EntityContext context)) return false;
            if (!context.Entity.TryGetComponent(out pickableResourceComponent)) return false;
            
            return true;
        }

        private bool TryPickup(Vector2 distanceVector, PickableResourceComponent pickableResourceComponent)
        {
            if (distanceVector.magnitude > pickupRadius) return false;
            
            pickableResourceComponent.Pickup();
            return true;

        }

        private void ApplyPullForce(PickableResourceComponent pickableResourceComponent, Vector2 distanceVector)
        {
            Rigidbody2D rb = pickableResourceComponent.Rb;
            rb.AddForce(distanceVector.normalized * Time.deltaTime * pullForce);
            Debug.Log($"{rb.name} + pulling");
        }
    }
}
