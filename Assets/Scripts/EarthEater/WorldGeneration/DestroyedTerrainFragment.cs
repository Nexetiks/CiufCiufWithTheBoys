using UnityEngine;

namespace EarthEater.WorldGeneration
{
    public class DestroyedTerrainFragment : MonoBehaviour
    {
        public void OnSpawn(Vector2 position)
        {
            transform.position = position;
        }
    }
}
