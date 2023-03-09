using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class FrustumObjectCulling : MonoBehaviour
{
    private static FrustumObjectCulling instance;
    public static FrustumObjectCulling Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FrustumObjectCulling>();
            }
            return instance;
        }
    }
    private Camera cam;
    private List<GameObject> objectsToCull = new List<GameObject>();
    private bool listChanged;
    private JobHandle jobHandle;
    private NativeArray<bool> isOffScreenResult; 
    private NativeArray<Vector2> screenPos;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        isOffScreenResult = new NativeArray<bool>(objectsToCull.Count, Allocator.Persistent);
        screenPos = new NativeArray<Vector2>(objectsToCull.Count, Allocator.Persistent);
        
        cam = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (screenPos == null || screenPos.Length == 0)
        {
            return;
        }
        for(int i = 0; i < screenPos.Length; i++)
        {
            screenPos[i] = cam.WorldToScreenPoint(objectsToCull[i].transform.position);
        }
        
        CullingJob2 cullingJob = new CullingJob2(screenPos, cam.pixelWidth, cam.pixelHeight, 400, isOffScreenResult);
        jobHandle = cullingJob.Schedule(objectsToCull.Count, 64);
    }

    private void LateUpdate()
    {
        jobHandle.Complete();
        
        for(int i = 0; i < isOffScreenResult.Length; i++)
        {
            objectsToCull[i].SetActive(!isOffScreenResult[i]);
        }
        
        if (listChanged)
        {
            isOffScreenResult.Dispose();
            isOffScreenResult = new NativeArray<bool>(objectsToCull.Count, Allocator.Persistent);
            screenPos.Dispose();
            screenPos = new NativeArray<Vector2>(objectsToCull.Count, Allocator.Persistent);
            listChanged = false;
        }
    }

    private void OnDestroy()
    {
        isOffScreenResult.Dispose();
        screenPos.Dispose();
    }

    public void AddObjectToCull(GameObject o)
    {
        objectsToCull.Add(o);
        listChanged = true;
    }
}