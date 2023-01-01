using System;

namespace Entities.Effects.Statuses
{
    public class EntityStatus : Effect<EntityStatusArgs>
    {
        public EntityStatus(string name, int startingDuration) : base(name, startingDuration)
        {
            Expired += OnExpired;
        }

        ~EntityStatus()
        {
            Expired -= OnExpired;
        }

        protected override void OnTrigger(EntityStatusArgs entityArgs)
        {
        }

        protected virtual void OnExpired(object sender, EventArgs e)
        {
        }
    }
}
