using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{

    public GameObject explosionPrefab;
    public float radius = 5;
    public float rotationSpeed = 30;

    private void Update()
    {
        transform.Rotate(Vector3.forward,rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        }
        //todo: destroy enemies in radius
        Destroy(gameObject);
    }
}
