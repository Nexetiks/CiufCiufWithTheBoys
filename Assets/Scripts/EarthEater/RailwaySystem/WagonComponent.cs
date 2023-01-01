using EarthEater.RailwaySystem.WagonEffects;
using Entities.Components;
using UnityEngine;

namespace EarthEater.RailwaySystem
{
    [System.Serializable]
    public class WagonComponent : BaseComponent
    {
        [field: SerializeField]
        public float Weight { get; private set; }
        
        [field: SerializeReference, ReferencePicker(TypeGrouping = TypeGrouping.ByFlatName), ReorderableList]
        public WagonEffect[] wagonEffects;

        public void OnAttached()
        {
            foreach (WagonEffect wagonEffect in wagonEffects)
            {
                wagonEffect.OnAttach(MyEntity);
            }
        }

        public void OnDetach()
        {
            foreach (WagonEffect wagonEffect in wagonEffects)
            {
                wagonEffect.OnDetach(MyEntity);
            }
        }
    }
}
