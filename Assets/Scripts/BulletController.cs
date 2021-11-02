using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime = 5f; // life of bullet in sec
    public float speed = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(DieAfterExpired));
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // todo: implement when there are collidables 
    }

    private IEnumerator DieAfterExpired()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}