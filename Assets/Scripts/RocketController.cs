using System.Collections;
using System.Collections.Generic;
using Enemy;
using EZCameraShake;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public GameObject explosionPrefab;
    
    public float speed = 20f;
    public float radius = 4;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    
    private void Explode(Vector3 explosionPos, float explosionRadius)
    {
        CameraShaker.Instance.ShakeOnce(4.0f, 4.0f, 0.1f, 1.0f);
        Instantiate(explosionPrefab, explosionPos, explosionPrefab.transform.rotation);
        
        var colliders = Physics2D.OverlapCircleAll(explosionPos, explosionRadius);
        
        foreach (var collider in colliders)
        {
            var enemyController = collider.gameObject.GetComponent<AbstractEnemy>();
            
            if (collider.gameObject.CompareTag("Enemy") && enemyController != null)
            {
                enemyController.InitiateDestroy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Wall")) return;
        Explode(transform.position, radius);
        Destroy(gameObject);
    }
}
