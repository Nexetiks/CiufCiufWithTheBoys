namespace Common.Util
{
    public class SuccessFriendlyRandomChance
    {
        private readonly float successChance;
        private readonly float increaseFactor;
        private float currentChance;

        public SuccessFriendlyRandomChance(float successChance, float increaseFactor) 
        {
            this.successChance = successChance;
            this.increaseFactor = increaseFactor;
            
            currentChance = successChance;
        }

        public bool Roll() 
        {
            bool success = UnityEngine.Random.value <= currentChance;
            if (!success) 
            {
                currentChance += increaseFactor;
            } 
            else 
            {
                currentChance = successChance;
            }
            return success;
        }
    }
}
