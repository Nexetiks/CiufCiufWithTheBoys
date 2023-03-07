using UnityEngine;

namespace Entities
{
    public enum EntityTag
    {
        Player,
        Enemy,
        Environment
    }

    public class EntityContext : MonoBehaviour
    {
        [SerializeField]
        private EntityDefaultDataSO entityDefaultDataSo;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [field: SerializeField]
        public EntityTag EntityTag { get; private set; }

        public Entity Entity { get; private set; }

        private void Awake()
        {
            if(entityDefaultDataSo == null) return;
            Initialize(entityDefaultDataSo);
        }
        
        public void Initialize(EntityDefaultDataSO entityDefaultDataSo)
        {
            this.entityDefaultDataSo = entityDefaultDataSo;
            Entity = new Entity(this.entityDefaultDataSo.EntityDefaultData, gameObject);

            if (this.entityDefaultDataSo.EntityDefaultData.Sprite == null) return;
            
            spriteRenderer.sprite = this.entityDefaultDataSo.EntityDefaultData.Sprite;
        }

        private void Update()
        {
            Entity.Update();
        }

        private void FixedUpdate()
        {
            Entity.FixedUpdate();
        }
        
        private void LateUpdate()
        {
            Entity.LateUpdate();
        }
    }
}