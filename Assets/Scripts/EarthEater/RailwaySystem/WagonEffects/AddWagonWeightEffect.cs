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
            entity.TryGetComponent(out engineComponent);
            weightModifier = new StatModifier(MyWagonComponent.Weight, "AddWagonWeightModifier");
            //TODO: Add weightModifier to the EngineComponent's forwardForce
        }

        public override void OnDetach(Entity entity)
        {
            //TODO: Remove weightModifier from the EngineComponent
        }
    }
}