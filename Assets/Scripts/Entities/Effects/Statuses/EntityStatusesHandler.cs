using System;
using System.Collections.Generic;

namespace Entities.Effects.Statuses
{
    public class EntityStatusesHandler
    {
        public event Action<EntityStatus> OnStatusRemoved;
        public event Action<EntityStatus> OnStatusAdded;
        
        private Dictionary<Type, EntityStatus> appliedStatuses = new Dictionary<Type, EntityStatus>();
        private Dictionary<Type, EntityStatus> statusesToTrigger = new Dictionary<Type, EntityStatus>();
        private EntityStatusArgs statusArgs;

        public Entity Target { get; set; }

        public EntityStatusesHandler(Entity target)
        {
            Target = target;
            statusArgs = new EntityStatusArgs(target);
        }

        public void TriggerStatuses()
        {
            foreach (EntityStatus status in statusesToTrigger.Values)
            {
                status.Trigger(statusArgs);
                if (status.Duration == 0)
                {
                    appliedStatuses.Remove(status.GetType());
                    statusesToTrigger.Remove(status.GetType());
                    OnStatusRemoved?.Invoke(status);
                }
            }
        }

        // If status duration is set to Instant (0) it will be triggered immediately and will not be triggered using
        // the TriggerStatuses method
        public void AddStatus(EntityStatus status)
        {
            if (appliedStatuses.ContainsKey(status.GetType()))
            {
                //refresh status
                appliedStatuses[status.GetType()].Duration = status.Duration;
            }
            else
            {
                appliedStatuses.Add(status.GetType(), status);
                if (status.Duration != EntityStatus.INSTANT)
                {
                    statusesToTrigger.Add(status.GetType(), status);
                    status.Trigger(statusArgs);
                }
                OnStatusAdded?.Invoke(status);
            }
        }
        
        public void RemoveStatus<T>() where T: EntityStatus
        {
            if (appliedStatuses.ContainsKey(typeof(T)))
            {
                EntityStatus status = appliedStatuses[typeof(T)];
                appliedStatuses.Remove(typeof(T));
                OnStatusRemoved?.Invoke(status);
            }
        }
    }
}
