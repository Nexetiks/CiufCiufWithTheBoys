namespace Entities.Effects.Statuses
{
    public class EntityStatus : ContinuousEffect<EntityStatusArgs>
    {
        public EntityStatus(string name, int startingDuration) : base(name, startingDuration)
        {
        }

        protected override void OnPerform(EntityStatusArgs entityArgs)
        {
        }
    }
}