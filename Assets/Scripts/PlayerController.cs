using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public Camera mainCamera;
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
    
    public float moveSpeed = 10f;
    public int framesBetweenShots = 30;
    
    private bool shootStarted = false;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            shootStarted = true;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            shootStarted = false;
            tempFrameCount = 0; // reset the counter
        }

        ShootIfStarted();
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

    private int tempFrameCount = 0;
    
    void ShootIfStarted()
    {
        if (!shootStarted)
            return;
        
        if (tempFrameCount == 0)
            Shoot();
        
        ++tempFrameCount;
        if (tempFrameCount == framesBetweenShots)
            tempFrameCount = 0;
    }
    
    void Shoot()
    {
        var liftVector = new Vector3(0, 0, 0.1f);
        Instantiate(bulletPrefab, leftFirePoint.position + liftVector, leftFirePoint.rotation);
        Instantiate(bulletPrefab, rightFirePoint.position + liftVector, rightFirePoint.rotation);
    }
}