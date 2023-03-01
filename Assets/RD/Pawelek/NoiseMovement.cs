using System;
using Common.Util;
using Plugins.FastNoiseLite;
using UnityEngine;

namespace RD.Pawelek
{
    public class NoiseMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private float noiseScrollSpeed;
        [SerializeField]
        private float maxAngle = 10f;
        [SerializeField]
        private float movementSpeed = 1;
        [SerializeField]
        private float minDistanceToStartGoingBack;
        [SerializeField]
        private float maxDistanceFromStartingPoint;

        private Vector2 direction;
        private Vector2 startingPoint;
        private NoiseValueProvider noiseValueProvider;

        private void Start()
        {
            startingPoint = gameObject.transform.position;
            direction = transform.up;
            noiseValueProvider = new NoiseValueProvider(FastNoiseLite.NoiseType.OpenSimplex2, scrollSpeed: noiseScrollSpeed);
        }

        private void Update()
        {
            noiseValueProvider.ScrollSpeed = noiseScrollSpeed; // TODO remove when game finished;

            float noiseValue = noiseValueProvider.Evaluate();

            Vector2 noiseDirection = CalculateNoiseForce(noiseValue);
            Vector2 actualSpawnDirectionForce = CalculateSpawnDirectionForce(out float distanceFromSpawnPoint);
            direction = CalculateFinalDirection(distanceFromSpawnPoint, noiseDirection, actualSpawnDirectionForce);

            gameObject.transform.up = direction;
            rb.velocity = direction * movementSpeed;
        }

        private Vector2 CalculateNoiseForce(float noiseValue)
        {
            Quaternion rotationAngle = Quaternion.Euler(new Vector3(0f, 0f, noiseValue * maxAngle));
            Vector2 noiseDirection = rotationAngle * direction;
            return noiseDirection;
        }

        private Vector2 CalculateSpawnDirectionForce(out float distanceFromSpawnPoint)
        {
            Vector2 distanceToSpawnPoint = startingPoint - (Vector2)transform.position;
            Vector2 directionToSpawnPoint = distanceToSpawnPoint.normalized;

            float angleBetweenSpawnPointDirAndDir = Vector2.SignedAngle(direction, directionToSpawnPoint);
            Vector2 actualSpawnDirectionForce = Quaternion.Euler(new Vector3(0f, 0f, angleBetweenSpawnPointDirAndDir)) * direction;
            distanceFromSpawnPoint = distanceToSpawnPoint.magnitude;
            return actualSpawnDirectionForce;
        }

        private Vector2 CalculateFinalDirection(float distanceFromSpawnPoint, Vector2 noiseDirection, Vector2 actualSpawnDirectionForce)
        {
            float t = Math.Clamp(((distanceFromSpawnPoint - minDistanceToStartGoingBack) / (maxDistanceFromStartingPoint - minDistanceToStartGoingBack)), 0f, 1f);
            direction = Vector2.Lerp(noiseDirection, actualSpawnDirectionForce, t);
            return direction;
        }
    }
}