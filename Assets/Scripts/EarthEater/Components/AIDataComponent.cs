using System;
using Entities;
using Entities.Components;
using UnityEngine;

namespace EarthEater.Components
{
    [Serializable]
    public class AIDataComponent : BaseComponent
    {
        [field: SerializeField]
        public EntityContext Target { get; set; }
        [field: SerializeField]
        public bool isEscaping { get; set; }
    }
}