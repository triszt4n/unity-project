using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using EZCameraShake;
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
        CameraShaker.Instance.ShakeOnce(4.0f, 4.0f, 0.1f, 1.0f);
        Instantiate(explosionPrefab, where, explosionPrefab.transform.rotation);
        var destroyableColliders = Physics2D.OverlapCircleAll(where, howBig);
        foreach (var toDestroyCollider in destroyableColliders)
        {
            var enemyController = toDestroyCollider.gameObject.GetComponent<AbstractEnemy>();
            if (toDestroyCollider.gameObject.CompareTag("Enemy") && enemyController!= null)
            {
                enemyController.InitiateDestroy();
            }
        }
    }
}
