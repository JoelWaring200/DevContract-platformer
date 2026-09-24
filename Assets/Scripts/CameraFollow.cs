using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //player tracker
    private Transform target;

    //movement
    public float smoothRate = 1.5f;
    //pos
    public Vector3 cameraOffset = Vector3.zero;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
    {
        //movement
        Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos + cameraOffset, Time.deltaTime * smoothRate);
    }
}
