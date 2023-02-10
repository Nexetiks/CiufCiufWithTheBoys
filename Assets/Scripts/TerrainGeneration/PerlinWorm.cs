using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinWorm
{
    private Vector2 currentDirection;
    private Vector2 currentPosition;
    private float maxAngle;
    
    public PerlinWorm(Vector2 startPosition, float maxAngle)
    {
        currentPosition = startPosition;
        this.maxAngle = maxAngle;
        currentDirection = Random.insideUnitCircle.normalized;
    }

    public Vector2 Move()
    {
        Vector3 direction = GetPerlinNoiseDirection();
        currentPosition += (Vector2)direction;
        return currentPosition;
    }

    private Vector3 GetPerlinNoiseDirection()
    {
        float noise = Mathf.PerlinNoise(currentPosition.x, currentPosition.y);
        float noiseDegrees = Mathf.Lerp(-maxAngle, maxAngle, noise);
        currentDirection = (Quaternion.Euler(0, 0, noiseDegrees) * currentDirection).normalized;
        return currentDirection;
    }
}
