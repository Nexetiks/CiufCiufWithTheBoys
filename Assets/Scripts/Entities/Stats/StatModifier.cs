namespace Entities
{
    public class StatModifier
    {
        public float Value { get; private set; }
        public string Source { get; private set; }

        public StatModifier(float value, string source)
        {
            this.Value = value;
            this.Source = source;
        }
    }
}