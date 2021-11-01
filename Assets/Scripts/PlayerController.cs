using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Camera camera;
    public Transform rightFirePoint;
    public Transform leftFirePoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
    }

    private Vector2 movement;
    private Vector2 mousePos;
    
    private float moveSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
            Shoot();
    }

    // src: https://www.youtube.com/watch?v=LNLVOjbrQj4&t=207s
    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        // Setting lookDir at mouse
        Vector2 lookDir = mousePos - playerRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        playerRb.rotation = angle;
    }
    
    void Shoot()
    {
        var liftVector = new Vector3(0, 0, 0.1f);
        GameObject leftBullet = Instantiate(bulletPrefab, leftFirePoint.position + liftVector, leftFirePoint.rotation);
        GameObject rightBullet = Instantiate(bulletPrefab, rightFirePoint.position + liftVector, rightFirePoint.rotation);
    }
}