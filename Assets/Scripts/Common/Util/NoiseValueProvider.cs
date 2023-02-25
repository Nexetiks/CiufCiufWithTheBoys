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

        public NoiseValueProvider(FastNoiseLite.NoiseType noiseType, int seed = 0, float frequency = 0.01f, float scrollSpeed = 0, int noiseOctaves = 1)
        {
            noise = new FastNoiseLite();
            noise.SetSeed(seed);
            noise.SetFrequency(frequency);
            noise.SetFractalOctaves(noiseOctaves);
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

        public float GetNoise01(float x, float y)
        {
            return (GetNoise(x, y)+1)*.5f;
        }

        public float Evaluate()
        {
            float noiseValue = noise.GetNoise(x, y);
            x += ScrollSpeed * Time.deltaTime;
            return noiseValue;
        }
    }
}