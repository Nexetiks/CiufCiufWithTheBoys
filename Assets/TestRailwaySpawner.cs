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

    private List<WagonComponent> wagons = new List<WagonComponent>();
    private void Awake()
    {
        Spawn();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && wagons.Count>0)
        {
            WagonComponent lastWagon = wagons[wagons.Count - 1];
            wagons.RemoveAt(wagons.Count-1);
            lastWagon.OnDetach();
            lastWagon.NextWagon = null;
        }
    }

    private void Spawn()
    {
        EntityContext frontInstance = Instantiate(frontPrefab);
        WagonComponent frontWagonComponent = frontInstance.Entity.GetComponent<WagonComponent>();
        frontWagonComponent.WagonHeadEntity = frontInstance.Entity;

        frontWagonComponent.OnAttached();

        WagonComponent frontalWagon = frontWagonComponent;

        for (int i = 0; i < wagonsAmount; i++)
        {
            EntityContext wagonInstance = Instantiate(wagonPrefab);
            wagonInstance.gameObject.name += i.ToString();
            WagonComponent wagonComponent = wagonInstance.Entity.GetComponent<WagonComponent>();
            wagonComponent.WagonHeadEntity = frontInstance.Entity;
            wagons.Add(wagonComponent);
            frontalWagon.PreviousWagon = wagonComponent;
            wagonComponent.NextWagon = frontalWagon;
            frontalWagon = wagonComponent;
            wagonComponent.OnAttached();
        }
    }
}
