using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform targetToFollow;

    [SerializeField] float cameraSpeed;
 
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        targetToFollow = GameObject.Find("CameraLookAt").transform;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = targetToFollow.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);
        transform.position = smoothedPosition;        
    }
    private void LateUpdate()
    { /*
        */
    }
}
