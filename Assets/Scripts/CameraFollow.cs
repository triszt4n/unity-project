using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject followedObject;

    // Start is called before the first frame update
    void Start()
    {
        followedObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        // simple follow behaviour, might change to something more fancy later;
        if (followedObject == null) return;
        transform.position = new Vector3(followedObject.transform.position.x, followedObject.transform.position.y,
            transform.position.z);
    }
}