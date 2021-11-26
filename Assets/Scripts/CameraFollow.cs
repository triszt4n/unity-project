using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject followedObject;
    public float timeOffset = 10;
    public float leftLimit = -44;
    public float rightLimit = 44;
    public float topLimit = 23;
    public float bottomLimit = 23;
    
    // Start is called before the first frame update
    void Start()
    {
        followedObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (followedObject == null) return;

        Vector3 startPos = transform.position;
        Vector3 endPos = followedObject.transform.position;
        endPos.z = transform.position.z;
        
        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.fixedDeltaTime);
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
        );
    }
}