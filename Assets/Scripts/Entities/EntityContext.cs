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
            Entity = new Entity(entityDefaultDataSo.EntityDefaultData);
        }

        private void Update()
        {
            Entity.Update();
        }
    }
}
