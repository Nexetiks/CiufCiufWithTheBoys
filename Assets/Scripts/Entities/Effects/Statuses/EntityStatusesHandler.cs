using System;
using System.Collections.Generic;

namespace Entities.Effects.Statuses
{
    public class EntityStatusesHandler
    {
        public event Action<EntityStatus> OnStatusRemoved;
        public event Action<EntityStatus> OnStatusAdded;
        
        private Dictionary<Type, EntityStatus> appliedStatuses = new Dictionary<Type, EntityStatus>();
        private EntityStatusArgs statusArgs;

        public Entity Target { get; set; }

        public EntityStatusesHandler(Entity target)
        {
            Target = target;
            statusArgs = new EntityStatusArgs(target);
        }

        public void UpdateStatuses()
        {
            foreach (EntityStatus status in appliedStatuses.Values)
            {
                status.Perform(statusArgs);
                if (status.IsInfinite || !status.ExpirationConditions)
                {
                    continue;
                }
                
                appliedStatuses.Remove(status.GetType());
                OnStatusRemoved?.Invoke(status);
            }
        }
        
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
