using Plugins.FastNoiseLite;
using UnityEngine;

namespace Common.Util
{
    public class NoiseValueProvider
    {
        private FastNoiseLite noise;
        private float x;
        private float y;

        public float ScrollSpeed { get; set; }

        public NoiseValueProvider(FastNoiseLite.NoiseType noiseType, float scrollSpeed)
        {
            noise = new FastNoiseLite();
            noise.SetNoiseType(noiseType);
            this.ScrollSpeed = scrollSpeed;
        }

        public float GetNoise(float x, float y)
        {
            this.x = x;
            this.y = y;

            float noiseValue = noise.GetNoise(x, y);
            return noiseValue;
        }

        public float Evaluate()
        {
            float noiseValue = noise.GetNoise(x, y);
            x += ScrollSpeed * Time.deltaTime;
            return noiseValue;
        }
    }
}