using Entities;
using Entities.Components;
using UnityEngine;

public class DefaultAttackStatsComponent : BaseComponent
{
    [field: SerializeField]
    public Stat Cooldown { get; private set; }

    public override object Clone()
    {
        DefaultAttackStatsComponent attckComponent = (DefaultAttackStatsComponent)base.Clone();
        attckComponent.Cooldown = new Stat(Cooldown.BaseValue);

        return attckComponent;
    }
}