using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public Camera mainCamera;
    public Transform rightFirePoint;
    public Transform leftFirePoint;
    public GameObject bulletPrefab;
    public HealthBar hpBar;

    public int maxHealth = 3;
    public int health = 2;

    public bool hasShield = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        UpdateHealthUI();
    }

    private Vector2 movement;
    private Vector2 mousePos;
    
    public float moveSpeed = 10f;
    public int millisBetweenShots = 200;
    private DateTime lastShot = DateTime.Now;
    
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
    
    private void ShootIfStarted()
    {
        if (!shootStarted)
            return;

        long timeSinceLastShot = (long) (DateTime.Now - lastShot).TotalMilliseconds;

        if (timeSinceLastShot >= millisBetweenShots)
        {
            lastShot = DateTime.Now;
            Shoot();
        }
        
    }
    
    private void Shoot()
    {
        var liftVector = new Vector3(0, 0, 0.1f);
        Instantiate(bulletPrefab, leftFirePoint.position + liftVector, leftFirePoint.rotation);
        Instantiate(bulletPrefab, rightFirePoint.position + liftVector, rightFirePoint.rotation);
    }

    public void UpdateHealthUI()
    {
        hpBar.UpdateHealth((float)this.health / this.maxHealth);
    }
    
}