using Entities;
using Entities.Components;

namespace EarthEater.Components
{
    public class GetFollowedByCameraComponent : BaseComponent
    {
        public override void Initialize(Entity myEntity)
        {
            base.Initialize(myEntity);
            if (CameraFollower.Instance != null)
            {
                CameraFollower.Instance.ObjectToFollow = myEntity.GameObject.transform;
            }
        }
    }
}
