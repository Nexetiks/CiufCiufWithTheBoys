namespace Entities.Effects
{
    public class MoveTriggeredEffect : TriggeredEffect<MoveEffectArgs>
    {
        public MoveTriggeredEffect(int startingDuration = -1) : base("default move", startingDuration)
        {
        }
        
        protected override void OnPerform(MoveEffectArgs args)
        {
        }
    }
}
