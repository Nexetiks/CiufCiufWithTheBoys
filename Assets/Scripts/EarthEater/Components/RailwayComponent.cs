using System.Collections;
using System.Collections.Generic;
using EarthEater.RailwaySystem;
using Entities.Components;
using UnityEngine;

public class RailwayComponent : BaseComponent
{
    [SerializeField]
    private List<WagonComponent> wagons = new List<WagonComponent>();
    
    public List<WagonComponent> Wagons => wagons;

    public void DetachLast()
    {
        if (wagons.Count <= 0) return;
        
        WagonComponent lastWagon = wagons[wagons.Count - 1];
        wagons.RemoveAt(wagons.Count - 1);
        lastWagon.OnDetach();
        lastWagon.NextWagon = null;
    }
}
