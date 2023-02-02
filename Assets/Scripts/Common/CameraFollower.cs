using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFollow;
    [SerializeField]
    private float smoothing = .8f;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 destination = objectToFollow.position;
        destination.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, destination, smoothing * Time.deltaTime);
    }
}