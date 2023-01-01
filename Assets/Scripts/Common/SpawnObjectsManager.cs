using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnObjectsManager : MonoBehaviour
{
    //[EditorButton(nameof(Spawn))]
    [SerializeField] private GameObject prefab;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float spacing;

    public void Spawn()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject instance = (GameObject) PrefabUtility.InstantiatePrefab(prefab);
                instance.transform.SetParent(transform);
                instance.transform.localPosition = new Vector3(j * spacing, i * spacing);
            }
        }
    }

}
