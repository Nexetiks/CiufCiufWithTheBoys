using System.Collections.Generic;
using UnityEngine;

public class FrustumObjectCulling : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private List<GameObject> objectsToCull = new List<GameObject>();

    private void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject o in objectsToCull)
        {
            // test planes against object bounds
            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(cam), o.GetComponent<Renderer>().bounds))
            {
                o.SetActive(true);
            }
            else
            {
                o.SetActive(false);
            }
        }
    }
}