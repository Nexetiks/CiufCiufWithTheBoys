using UnityEngine;

namespace Entities
{
    public enum EntityTag
    {
        Player,
        Enemy,
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
            Entity = new Entity(entityDefaultDataSo.EntityDefaultData, gameObject);
            spriteRenderer.sprite = entityDefaultDataSo.EntityDefaultData.Sprite;
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