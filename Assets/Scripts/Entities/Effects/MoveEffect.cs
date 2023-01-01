namespace Entities.Effects
{
    public class MoveEffect : Effect<MoveEffectArgs>
    {
        public MoveEffect(int startingDuration = -1) : base("default move", startingDuration)
        {
        }
        
        protected override void OnTrigger(MoveEffectArgs args)
        {
        }
    }
}
