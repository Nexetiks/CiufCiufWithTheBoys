using System;
using Common.AIBase;
using Common.Util;
using Entities.Abilities;
using Entities.Abilities.DefaultMove;
using Plugins.FastNoiseLite;
using UnityEngine;

namespace Entities.AI
{
    public class PatrolNode : Node
    {
        private EntityContext ai;
        private Rigidbody2D rb;
        private float noiseScrollSpeed;
        private float maxAngle = 10f;
        private float movementSpeed = 1;
        private float minDistanceToStartGoingBack;
        private float maxDistanceFromStartingPoint;

        private AbilitiesHandler abilitiesHandler;
        private Vector2 direction;
        private Vector2 startingPoint;
        private NoiseValueProvider noiseValueProvider;

        public PatrolNode(EntityContext ai, Rigidbody2D rb, float noiseScrollSpeed, float maxAngle, float movementSpeed, float minDistanceToStartGoingBack, float maxDistanceFromStartingPoint, AbilitiesHandler abilitiesHandler)
        {
            this.ai = ai;
            this.rb = rb;
            this.noiseScrollSpeed = noiseScrollSpeed;
            this.maxAngle = maxAngle;
            this.movementSpeed = movementSpeed;
            this.minDistanceToStartGoingBack = minDistanceToStartGoingBack;
            this.maxDistanceFromStartingPoint = maxDistanceFromStartingPoint;
            this.abilitiesHandler = abilitiesHandler;

            startingPoint = ai.Entity.GameObject.transform.position;
            direction = ai.Entity.GameObject.transform.up;
            noiseValueProvider = new NoiseValueProvider(FastNoiseLite.NoiseType.OpenSimplex2, scrollSpeed: noiseScrollSpeed);
        }

        public override NodeState Evaluate()
        {
            noiseValueProvider.ScrollSpeed = noiseScrollSpeed; // TODO remove when game finished;

            float noiseValue = noiseValueProvider.Evaluate();

            Vector2 noiseDirection = CalculateNoiseForce(noiseValue);
            Vector2 actualSpawnDirectionForce = CalculateSpawnDirectionForce(out float distanceFromSpawnPoint);
            direction = CalculateFinalDirection(distanceFromSpawnPoint, noiseDirection, actualSpawnDirectionForce);

            abilitiesHandler.PerformAbility<DefaultMoveAbility>(new DefaultMoveAbilityArgs(direction));

            //ai.Entity.GameObject.transform.up = direction;
            // rb.velocity = direction * (movementSpeed * Time.deltaTime);

            return NodeState.Running;
        }

        private Vector2 CalculateNoiseForce(float noiseValue)
        {
            Quaternion rotationAngle = Quaternion.Euler(new Vector3(0f, 0f, noiseValue * maxAngle));
            Vector2 noiseDirection = rotationAngle * direction;
            return noiseDirection;
        }

        private Vector2 CalculateSpawnDirectionForce(out float distanceFromSpawnPoint)
        {
            Vector2 distanceToSpawnPoint = startingPoint - (Vector2)ai.Entity.GameObject.transform.position;
            Vector2 directionToSpawnPoint = distanceToSpawnPoint.normalized;

            float angleBetweenSpawnPointDirAndDir = Mathf.Clamp(Vector2.SignedAngle(direction, directionToSpawnPoint), -maxAngle, maxAngle);
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