namespace Entities.Effects.Statuses
{
    public class EntityStatusArgs : EffectArgs
    {
        public Entity Target { get; set; }

        public EntityStatusArgs(Entity target)
        {
            Target = target;
        }
    }
}