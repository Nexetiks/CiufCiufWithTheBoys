using UnityEngine;

namespace Entities
{
    public class EntityContext : MonoBehaviour
    {
        [SerializeField]
        private EntityDefaultDataSO entityDefaultDataSo;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public Entity Entity { get; private set; }

        static int i =0;
        private void Awake()
        {
            Debug.Log(i);
            i++;
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