using Entities.Effects;

namespace EarthEater.Abilities.MoveEngine
{
    public class MoveEngineAbilityArgs : DefaultAbilityArgs
    {
        public int Dir { get; set; }

        public MoveEngineAbilityArgs(int dir)
        {
            Dir = dir;
        }
    }
}