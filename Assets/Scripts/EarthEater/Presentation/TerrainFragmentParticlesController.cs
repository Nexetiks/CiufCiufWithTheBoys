using System.Collections.Generic;
using Common.Util;
using EarthEater.WorldGeneration;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace EarthEater.Presentation
{
    public class TerrainFragmentParticlesController : MonoBehaviour
    {
        private static readonly ExposedProperty PositionProperty = "position";
        private static readonly ExposedProperty ColorProperty = "color";
    
        [SerializeField]
        private WorldGeneratorController worldGeneratorController;

        [SerializeField]
        private VisualEffect visualEffect;

        private VFXEventAttribute eventAttribute;

        private void Awake()
        {
            eventAttribute =  visualEffect.CreateVFXEventAttribute();
        }

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
            Dictionary<DestroyedTerrainFragment, int> prefabToParticlesAmountCache = new Dictionary<DestroyedTerrainFragment, int>();
            Vector2 position = transform.position;
            foreach (TerrainDestructionData destructionData in terrainDestroyedEvent.TerrainDestructionData)
            {
                if (destructionData.TerrainData.DestroyedTerrainFragment == null) continue;

                if(prefabToParticlesAmountCache.ContainsKey(destructionData.TerrainData.DestroyedTerrainFragment))
                {
                    prefabToParticlesAmountCache[destructionData.TerrainData.DestroyedTerrainFragment] += 1;
                    continue;
                }
                prefabToParticlesAmountCache.Add(destructionData.TerrainData.DestroyedTerrainFragment, 0);
            
                position = worldGeneratorController.TerrainDataPixelToWorldPosition(destructionData.X, destructionData.Y);
                Color color = SpritesUtil.GetPixelFromSprite(destructionData.TerrainData.Sprite,destructionData.X, destructionData.Y);
                Vector3 vector3Color = SpritesUtil.ColorToVector3(color);
                eventAttribute.SetVector3(ColorProperty, vector3Color);
                eventAttribute.SetVector3(PositionProperty, position);
                visualEffect.Play(eventAttribute);
            
            }
            transform.position = position;
        }
    }
}
