using System;
using Common.AIBase;
using Common.Util;
using EarthEater.Components;
using Entities.Abilities;
using Entities.Abilities.LimitedMove;
using Plugins.FastNoiseLite;
using UnityEngine;

namespace Entities.AI
{
    public class EscapeNode : Node
    {
        private EntityContext ai;
        private float noiseScrollSpeed;
        private float maxAngle = 10f;
        private float minDistanceToStartGoingBack;
        float maxDistanceFromStartingPoint;

        private AbilitiesHandler abilitiesHandler;
        private Vector2 direction;
        private Vector2 startingPoint;
        private NoiseValueProvider noiseValueProvider;
        private AIDataComponent aiDataComponent;

        public EscapeNode(EntityContext ai, float noiseScrollSpeed, float maxAngle, float minDistanceToStartGoingBack, float maxDistanceFromStartingPoint)
        {
            this.ai = ai;
            this.noiseScrollSpeed = noiseScrollSpeed;
            this.maxAngle = maxAngle;
            this.minDistanceToStartGoingBack = minDistanceToStartGoingBack;
            this.maxDistanceFromStartingPoint = maxDistanceFromStartingPoint;

            abilitiesHandler = ai.Entity.GetComponent<AbilitiesHandler>();

            startingPoint = ai.Entity.GameObject.transform.position;
            direction = ai.Entity.GameObject.transform.up;
            noiseValueProvider = new NoiseValueProvider(FastNoiseLite.NoiseType.OpenSimplex2, scrollSpeed: noiseScrollSpeed);
            aiDataComponent = ai.Entity.GetComponent<AIDataComponent>();
        }

        public override NodeState Evaluate()
        {
            noiseValueProvider.ScrollSpeed = noiseScrollSpeed; // TODO remove when game finished;

            float noiseValue = noiseValueProvider.Evaluate();

            Vector2 noiseDirection = CalculateNoiseForce(noiseValue);
            Vector2 actualSpawnDirectionForce = CalculateSpawnDirectionForce(out float distanceFromSpawnPoint);
            direction = CalculateFinalDirection(distanceFromSpawnPoint, noiseDirection, actualSpawnDirectionForce);

            abilitiesHandler.PerformAbility<LimitedMoveAbility>(new LimitedMoveAbilityArgs(direction));

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