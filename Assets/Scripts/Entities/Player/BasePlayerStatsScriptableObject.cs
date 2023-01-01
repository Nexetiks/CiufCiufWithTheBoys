using UnityEngine;

namespace Entities.Player
{
    [CreateAssetMenu(fileName = "Base Player Stats", menuName = "Base Player Stats")]
    public class BasePlayerStatsScriptableObject : ScriptableObject
    {
        [field : SerializeField]
        public float MaxSpeed { get; private set; }
        [field : SerializeField]
        public float ForwardForce { get; private set; }
        [field : SerializeField]
        public float RotationSpeed { get; private set; }
    }
}
