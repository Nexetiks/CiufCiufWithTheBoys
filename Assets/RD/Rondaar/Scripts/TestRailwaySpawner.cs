using System.Collections.Generic;
using EarthEater.Components;
using EarthEater.RailwaySystem;
using Entities;
using UnityEngine;

public class TestRailwaySpawner : MonoBehaviour
{
    [SerializeField]
    private EntityDefaultDataSO railwayData;
    [SerializeField]
    private EntityContext frontPrefab;
    [SerializeField]
    private EntityContext wagonPrefab;
    [SerializeField, ReorderableList]
    private EntityDefaultDataSO[] wagonDefaultData;
    [SerializeField]
    private TestGoldCounter goldCounter;
    
    private Entity railwayEntity;
    private RailwayComponent railwayComponent;

    private void Awake()
    {
        railwayEntity = new Entity(railwayData.EntityDefaultData, gameObject);
        railwayComponent = railwayEntity.GetComponent<RailwayComponent>();
        goldCounter.Initialize(railwayComponent.MyEntity.GetComponent<InventoryComponent>().Inventory);
        Spawn();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            railwayComponent.DetachLast();
        }
    }

    private void Spawn()
    {
        EntityContext frontInstance = Instantiate(frontPrefab);
        frontInstance.transform.position = transform.position;
        WagonComponent frontWagonComponent = frontInstance.Entity.GetComponent<WagonComponent>();
        frontWagonComponent.WagonHeadEntity = frontInstance.Entity;
        frontWagonComponent.RailwayComponent = railwayComponent;
        frontWagonComponent.OnAttached();

        WagonComponent frontalWagon = frontWagonComponent;

        for (int i = 0; i < wagonDefaultData.Length; i++)
        {
            EntityContext wagonInstance = Instantiate(wagonPrefab);
            wagonInstance.transform.position = transform.position;
            wagonInstance.Initialize(wagonDefaultData[i]);
            wagonInstance.gameObject.name += i.ToString();
            WagonComponent wagonComponent = wagonInstance.Entity.GetComponent<WagonComponent>();
            wagonComponent.WagonHeadEntity = frontInstance.Entity;
            wagonComponent.RailwayComponent = railwayComponent;
            railwayComponent.Wagons.Add(wagonComponent);
            frontalWagon.PreviousWagon = wagonComponent;
            wagonComponent.NextWagon = frontalWagon;
            frontalWagon = wagonComponent;
            wagonComponent.OnAttached();
        }
    }
}