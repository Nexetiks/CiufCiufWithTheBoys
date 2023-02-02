using System;

namespace Entities.Effects.Statuses
{
    public class EntityStatusesHandler
    {
        public event Action<EntityStatus> OnStatusRemoved;
        public event Action<EntityStatus> OnStatusAdded;

        private ContinuousEffectsUpdater<EntityStatusArgs> statusesUpdater;
        private EntityStatusArgs statusArgs;
        public Entity Target { get; set; }

        public EntityStatusesHandler(Entity target)
        {
            Target = target;
            statusArgs = new EntityStatusArgs(target);
            statusesUpdater = new ContinuousEffectsUpdater<EntityStatusArgs>();

            statusesUpdater.OnEffectAdded += StatusesUpdater_OnEffectAdded;
            statusesUpdater.OnEffectRemoved += StatusesUpdater_OnEffectRemoved;
        }

        ~EntityStatusesHandler()
        {
            statusesUpdater.OnEffectAdded -= StatusesUpdater_OnEffectAdded;
            statusesUpdater.OnEffectRemoved -= StatusesUpdater_OnEffectRemoved;
        }

        private void StatusesUpdater_OnEffectRemoved(ContinuousEffect<EntityStatusArgs> effect)
        {
            OnStatusRemoved?.Invoke(effect as EntityStatus);
        }

        private void StatusesUpdater_OnEffectAdded(ContinuousEffect<EntityStatusArgs> effect)
        {
            OnStatusAdded?.Invoke(effect as EntityStatus);
        }

        public void UpdateStatuses()
        {
            statusesUpdater.UpdateEffects(statusArgs);
        }

        public void AddStatus(EntityStatus status)
        {
            statusesUpdater.AddEffect(status);
        }

        public void RemoveStatus<T>() where T : EntityStatus
        {
            statusesUpdater.RemoveEffect<T>();
        }
    }
}