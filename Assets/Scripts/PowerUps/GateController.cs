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
        Explode(transform.position, radius);
        Destroy(gameObject);
    }

    private void Explode(Vector2 where, float howBig)
    {
        Instantiate(explosionPrefab, where, explosionPrefab.transform.rotation);
        var destroyableColliders = Physics2D.OverlapCircleAll(where, howBig);
        foreach (var toDestroyCollider in destroyableColliders)
        {
            if (toDestroyCollider.gameObject.CompareTag("Enemy"))
            {
                Destroy(toDestroyCollider.gameObject);
            }
        }
    }
}
