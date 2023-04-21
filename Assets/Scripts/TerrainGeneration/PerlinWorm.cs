using Common.Util;
using Plugins.FastNoiseLite;
using UnityEngine;

public class PerlinWorm
{
    private Vector2 currentDirection;
    private Vector2 currentPosition;
    private float maxAngle;
    private NoiseValueProvider noiseValueProvider;

    public PerlinWorm(Vector2 startPosition, float maxAngle)
    {
        currentPosition = startPosition;
        this.maxAngle = maxAngle;
        currentDirection = Random.insideUnitCircle.normalized;
        noiseValueProvider = new NoiseValueProvider(FastNoiseLite.NoiseType.Perlin);
    }

    public Vector2 Move()
    {
        Vector3 direction = GetPerlinNoiseDirection();
        currentPosition += (Vector2)direction;
        return currentPosition;
    }

    private Vector3 GetPerlinNoiseDirection()
    {
        float noise = noiseValueProvider.GetNoise(currentPosition.x, currentPosition.y);
        float noiseDegrees = noise * maxAngle;
        currentDirection = (Quaternion.Euler(0, 0, noiseDegrees) * currentDirection).normalized;
        return currentDirection;
    }
}