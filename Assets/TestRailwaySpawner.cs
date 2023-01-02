using System;
using System.Collections;
using System.Collections.Generic;
using EarthEater.RailwaySystem;
using Entities;
using UnityEngine;

public class TestRailwaySpawner : MonoBehaviour
{
    [SerializeField] private EntityContext frontPrefab;
    [SerializeField] private EntityContext wagonPrefab;
    [SerializeField] private int wagonsAmount;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        EntityContext frontInstance = Instantiate(frontPrefab);
        WagonComponent frontWagonComponent = frontInstance.Entity.GetComponent<WagonComponent>();

        frontWagonComponent.OnAttached();

        WagonComponent frontalWagon = frontWagonComponent;

        for (int i = 0; i < wagonsAmount; i++)
        {
            EntityContext wagonInstance = Instantiate(wagonPrefab);
            wagonInstance.gameObject.name += i.ToString();
            WagonComponent wagonComponent = wagonInstance.Entity.GetComponent<WagonComponent>();
            frontalWagon.PreviousWagon = wagonComponent;
            wagonComponent.NextWagon = frontalWagon;
            frontalWagon = wagonComponent;
            wagonComponent.OnAttached();
        }
    }
}
