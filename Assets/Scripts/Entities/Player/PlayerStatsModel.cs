namespace Entities.Player
{
    public class PlayerStatsModel
    {
        public Stat MaxSpeed { get; private set; }
        public Stat ForwardForce { get; private set; }
        public Stat RotationSpeed { get; private set; }

        public PlayerStatsModel(float baseSpeed, float baseForwardForce, float baseRotationSpeed)
        {
            MaxSpeed = new Stat(baseSpeed);
            ForwardForce = new Stat(baseForwardForce);
            RotationSpeed = new Stat(baseRotationSpeed);
        }

        public PlayerStatsModel(BasePlayerStatsScriptableObject basePlayerStatsScriptableObject)
        {
            MaxSpeed = new Stat(basePlayerStatsScriptableObject.MaxSpeed);
            ForwardForce = new Stat(basePlayerStatsScriptableObject.ForwardForce);
            RotationSpeed = new Stat(basePlayerStatsScriptableObject.RotationSpeed);
        }
    }
}
