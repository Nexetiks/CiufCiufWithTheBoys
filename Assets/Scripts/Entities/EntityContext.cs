using System;
using UnityEngine;

namespace Entities
{
    public class EntityContext : MonoBehaviour
    {
        [SerializeField] private EntityDefaultDataSO entityDefaultDataSo;

        public Entity Entity { get; private set; }
        
        private void Awake()
        {
            Entity = new Entity(entityDefaultDataSo.EntityDefaultData, gameObject);
        }

        private void Update()
        {
            Entity.Update();
        }

        private void FixedUpdate()
        {
            Entity.FixedUpdate();
        }
    }
}
