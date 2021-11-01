using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10;

    public float lifeTime = 5; //life of bullet in sec

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

    IEnumerator DieAfterExpired()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}