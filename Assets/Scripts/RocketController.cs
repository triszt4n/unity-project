using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using EZCameraShake;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public GameObject explosionPrefab;
    private Rigidbody2D rigidBody;

    public float accelerationFactor = 10f;
    public float explosionRadius = 5f;
    public float maxSpeed = 15f;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(transform.up * accelerationFactor, ForceMode2D.Force);

        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Wall")) return;
        Explode();
        Destroy(gameObject);
    }
    
    private void Explode()
    {
        var explosionPosition = transform.position;
        
        CameraShaker.Instance.ShakeOnce(4.0f, 4.0f, 0.1f, 1.0f);
        Instantiate(explosionPrefab, explosionPosition, explosionPrefab.transform.rotation);
        
        var colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);
        
        foreach (var c in colliders)
        {
            var enemyController = c.gameObject.GetComponent<AbstractEnemy>();
            
            if (c.gameObject.CompareTag("Enemy") && enemyController != null)
            {
                enemyController.InitiateDestroy();
            }
        }
    }
}
