using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 360;
    public float acceleration = 100;
    public GameObject bulletPrefab;

    private Rigidbody2D playerRB;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        transform.Rotate(-Vector3.forward, rotationSpeed * horizontalInput * Time.deltaTime);
        playerRB.AddForce(transform.up * acceleration * verticalInput * Time.deltaTime);
        
        if(Input.GetKeyDown(KeyCode.Space)) Shoot();
    }



    private void Shoot()
    {
        var pos = transform.position + new Vector3(0,0,0.1f);
        Instantiate(bulletPrefab, pos, transform.rotation);
    }
}