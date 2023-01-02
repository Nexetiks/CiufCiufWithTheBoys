using System;

namespace Entities.Effects
{
    public abstract class TriggeredEffect<TriggerArgs> : EffectBase<TriggerArgs> where TriggerArgs : EffectArgs
    {
        public static readonly int INFINITE = -1;
        public static readonly int INSTANT = 0;
        private int duration;

        public override bool ExpirationConditions
        {
            get
            {
                return Duration == 0;
            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                if (duration == value)
                {
                    return;
                }

                duration = value;
            }
        }

        public TriggeredEffect(string name, int startingDuration)
        {
            this.Name = name;
            this.duration = startingDuration;
        }

        protected override void OnPerform(TriggerArgs args)
        {
            if (Duration == 0)
            {
                return;
            }

            Duration = Duration == INFINITE
                ? INFINITE
                : Duration - 1;
        }

        public override string ToString() => Name + ": " +
                                             (Duration == INFINITE
                                                 ? "Infinite"
                                                 : Duration.ToString()) + " duration remaining";
    }
}