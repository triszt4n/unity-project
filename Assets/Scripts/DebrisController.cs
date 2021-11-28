using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DebrisController : MonoBehaviour
{

    public Rigidbody2D[] parts;
    public float force = 10f;
    public int lifetime = 5;
    
    void Start()
    {
        foreach (var part in parts)
        {
            var direction = Random.insideUnitCircle;
            part.AddForce(direction * force, ForceMode2D.Impulse);
        }

        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
