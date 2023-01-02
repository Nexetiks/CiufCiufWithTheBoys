using EarthEater.Components;
using Entities;

namespace EarthEater.RailwaySystem.WagonEffects
{
    public class AddWagonWeightEffect : WagonEffect
    {
        private StatModifier weightModifier;
        private EngineComponent engineComponent;

        public override void OnAttach(Entity entity)
        {
            MyWagonComponent.WagonHeadEntity.TryGetComponent(out engineComponent);
            weightModifier = new StatModifier(-MyWagonComponent.Weight*0.1f, "AddWagonWeightModifier");
            //TODO: Add weightModifier to the EngineComponent's forwardForce
            engineComponent.ForwardForce.AddModifier(weightModifier);
        }

        public override void OnDetach(Entity entity)
        {
            //TODO: Remove weightModifier from the EngineComponent
            engineComponent.ForwardForce.RemoveModifier(weightModifier);
        }
    }
}