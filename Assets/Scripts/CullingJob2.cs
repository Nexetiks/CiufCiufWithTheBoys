using Unity.Burst;
using Unity.Jobs;
using UnityEngine;
using Unity.Collections;

[BurstCompile]
public struct CullingJob2 : IJobParallelFor
{
    private int screenWidth;
    private int screenHeight;
    private int screenOffset;
    private NativeArray<bool> isOffScreenResult;
    private NativeArray<Vector2> screenPoints;

    public CullingJob2(NativeArray<Vector2> screenPoints, int screenWidth, int screenHeight, int screenOffset, NativeArray<bool> isOffScreenResult)
    {
        this.screenPoints = screenPoints;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.screenOffset = screenOffset;
        this.isOffScreenResult = isOffScreenResult;
    }

    public void Execute(int index)
    {
        HandleOffScreen(index);
    }

    private void HandleOffScreen(int index)
    {
        if(screenPoints[index].x < -screenOffset || screenPoints[index].x > screenWidth+screenOffset || screenPoints[index].y < -screenOffset || screenPoints[index].y > screenHeight+screenOffset)
        {
            isOffScreenResult[index] = true;
        }
        else
        {
            isOffScreenResult[index] = false;
        }
    }
}