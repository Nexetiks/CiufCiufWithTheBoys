namespace Entities.Effects
{
    public class EffectArgs
    {
        /// <summary>
        /// Whether or not the <see cref="T:GoRogue.EffectTrigger`1" /> should stop calling all subsequent effect's
        /// <see cref="M:GoRogue.Effect`1.Trigger(`0)" /> functions. See EffectTrigger's documentation for details.
        /// </summary>
        public bool CancelTrigger;

        /// <summary>Constructor.</summary>
        public EffectArgs() => this.CancelTrigger = false;
    }
}