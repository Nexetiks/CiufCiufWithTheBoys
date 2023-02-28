using System;
using System.Collections.Generic;
using Common.Util;
using UnityEngine;

namespace EarthEater.WorldGeneration
{
    public class DestroyedTerrainFragmentsSpawner : MonoBehaviour
    {
        [SerializeField]
        private WorldGeneratorController worldGeneratorController;

        private readonly Dictionary<DestroyedTerrainFragment, SuccessFriendlyRandomChance> prefabToRandomChanceCache = new Dictionary<DestroyedTerrainFragment, SuccessFriendlyRandomChance>();

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
            Dictionary<DestroyedTerrainFragment, DestroyedTerrainFragment> prefabToFragmentCache = new Dictionary<DestroyedTerrainFragment, DestroyedTerrainFragment>();

            foreach (TerrainDestructionData destructionData in terrainDestroyedEvent.TerrainDestructionData)
            {
                if (destructionData.TerrainData.DestroyedTerrainFragment == null) continue;

                if(!prefabToRandomChanceCache.TryGetValue(destructionData.TerrainData.DestroyedTerrainFragment, out SuccessFriendlyRandomChance randomChance))
                {
                    randomChance = new SuccessFriendlyRandomChance(destructionData.TerrainData.SpawnProbability, .0f);
                    prefabToRandomChanceCache.Add(destructionData.TerrainData.DestroyedTerrainFragment, randomChance);
                }

                if (!randomChance.Roll())
                {
                    continue;
                }
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
