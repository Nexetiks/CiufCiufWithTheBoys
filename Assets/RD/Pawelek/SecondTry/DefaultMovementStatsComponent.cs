using Entities;
using Entities.Components;
using UnityEngine;

public class DefaultMovementStatsComponent : BaseComponent
{
    [field: SerializeField]
    public Stat Speed { get; private set; }

    public override object Clone()
    {
        DefaultMovementStatsComponent wagonComponent = (DefaultMovementStatsComponent)base.Clone();
        wagonComponent.Speed = new Stat(Speed.BaseValue);

        return wagonComponent;
    }
}