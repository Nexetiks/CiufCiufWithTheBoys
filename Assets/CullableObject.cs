using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullableObject : MonoBehaviour
{
    private void Start()
    {
        FrustumObjectCulling.Instance.AddObjectToCull(gameObject);
    }
}
