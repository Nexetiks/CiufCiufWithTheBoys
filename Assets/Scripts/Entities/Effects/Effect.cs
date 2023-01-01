using System;

namespace Entities.Effects
{
  public abstract class Effect<TriggerArgs> where TriggerArgs : EffectArgs
  {
    public static readonly int INFINITE = -1;
    public static readonly int INSTANT = 0;
    private int _duration;

    /// <summary>The name of the effect.</summary>
    public string Name { get; set; }

    public int Duration
    {
      get => this._duration;
      set
      {
        if (this._duration == value)
          return;
        this._duration = value;
        if (this.Expired == null || this._duration != 0)
          return;
        this.Expired((object) this, EventArgs.Empty);
      }
    }
    
    public event EventHandler Expired;
    
    public Effect(string name, int startingDuration)
    {
      this.Name = name;
      this._duration = startingDuration;
    }

    public void Trigger(TriggerArgs args)
    {
      this.OnTrigger(args);
      if (this.Duration == 0)
        return;
      this.Duration = this.Duration == Effect<TriggerArgs>.INFINITE ? Effect<TriggerArgs>.INFINITE : this.Duration - 1;
    }
    
    protected abstract void OnTrigger(TriggerArgs e);
    
    public override string ToString() => this.Name + ": " + (this.Duration == Effect<TriggerArgs>.INFINITE ? "Infinite" : this.Duration.ToString()) + " duration remaining";
  }
}