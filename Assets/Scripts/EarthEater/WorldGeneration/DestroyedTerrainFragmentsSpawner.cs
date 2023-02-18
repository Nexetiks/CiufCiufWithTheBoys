using System.Collections.Generic;
using UnityEngine;

namespace EarthEater.WorldGeneration
{
    public class DestroyedTerrainFragmentsSpawner : MonoBehaviour
    {
        [SerializeField]
        private WorldGeneratorController worldGeneratorController;
    
        private void OnEnable()
        {
            OnTerrainDestroyedAetherEvent.AddListener(OnTerrainDestroyed);
        }
    
        private void OnDisable()
        {
            OnTerrainDestroyedAetherEvent.AddListener(OnTerrainDestroyed);
        }

        private void OnTerrainDestroyed(OnTerrainDestroyedAetherEvent terrainDestroyedEvent)
        {
            // TODO: OPTIMIZE, it's unacceptable to instantiate so many objects
            return;
            Dictionary<DestroyedTerrainFragment, DestroyedTerrainFragment> prefabToFragmentCache = new Dictionary<DestroyedTerrainFragment, DestroyedTerrainFragment>();
    
            foreach (TerrainDestructionData destructionData in terrainDestroyedEvent.TerrainDestructionData)
            {
                if (destructionData.TerrainData.DestroyedTerrainFragment == null) continue;

                if(prefabToFragmentCache.TryGetValue(destructionData.TerrainData.DestroyedTerrainFragment, out DestroyedTerrainFragment cachedFragment))
                {
                    cachedFragment.transform.localScale *= 1.05f;
                    continue;
                }
            
                DestroyedTerrainFragment fragment = Instantiate(destructionData.TerrainData.DestroyedTerrainFragment);
                Vector2 position = worldGeneratorController.TerrainDataPixelToWorldPosition(destructionData.X, destructionData.Y);
                fragment.OnSpawn(position);
                prefabToFragmentCache.Add(destructionData.TerrainData.DestroyedTerrainFragment, fragment);
            }
        }
    }
}
