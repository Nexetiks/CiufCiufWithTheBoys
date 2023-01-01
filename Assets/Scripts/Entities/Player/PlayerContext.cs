using System;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerContext : MonoBehaviour
    {
        [SerializeField] private BasePlayerStatsScriptableObject statsScriptableObject;
        
        public PlayerStatsModel PlayerStatsModel { get; private set; }

        private void Awake()
        {
            PlayerStatsModel = new PlayerStatsModel(statsScriptableObject);
        }
    }
}
