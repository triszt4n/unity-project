using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxController : MonoBehaviour
{
    private float startPos;
    public Camera cam;
    public float parallaxAmount;

    void Start()
    {
        startPos = transform.position.x;
    }

    void FixedUpdate()
    {
        var distance = cam.transform.position.x * parallaxAmount;
        transform.position = new Vector3(
            startPos + distance,
            transform.position.y,
            transform.position.z
        );
    }
}
