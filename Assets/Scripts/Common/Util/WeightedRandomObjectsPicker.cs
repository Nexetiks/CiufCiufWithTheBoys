using System.Collections.Generic;
using UnityEngine;

namespace Common.Util
{
    public class WeightedRandomObjectsPicker<T>
    {
        private List<T> objects = new List<T>();
        private List<uint> weights = new List<uint>();
        private uint maxWeight = 0;
        
        public void AddObject(T obj, uint weight)
        {
            objects.Add(obj);
            weights.Add(weight);
            maxWeight += weight;
        }
        
        public void RemoveObject(T obj)
        {
            int index = objects.IndexOf(obj);
            if (index == -1)
            {
                Debug.LogError($"Object {obj} not found in WeightedRandomObjectsPicker");
                return;
            }
            
            maxWeight -= weights[index];
            objects.RemoveAt(index);
            weights.RemoveAt(index);
        }

        public T GetObjectByChanceValue(float normalizedChanceValue)
        {
            if (normalizedChanceValue < 0 || normalizedChanceValue > 1)
            {
                Debug.LogError($"normalizedChanceValue must be in range [0, 1], but was {normalizedChanceValue}");
                return default;
            }
                
            float culminatedWeight = 0;
            for (int i = 0; i < objects.Count; i++)
            {
                culminatedWeight += weights[i] / (float)maxWeight;
                if (normalizedChanceValue <= culminatedWeight)
                    return objects[i];
            }

            return default;
        }
    }
}
