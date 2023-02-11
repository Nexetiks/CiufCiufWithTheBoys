using System;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [field: SerializeField]
    public Transform ObjectToFollow { get; set; }
    [SerializeField]
    private float smoothing = .8f;

    private static CameraFollower instance;
    
    public static CameraFollower Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CameraFollower>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(ObjectToFollow == null) return;
        
        Vector3 destination = ObjectToFollow.position;
        destination.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, destination, smoothing * Time.deltaTime);
    }
}