using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using Enemy;
using Unity.Collections;
using UnityEngine;
using EZCameraShake;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private AudioSource shootSource;

    public Camera mainCamera;
    public Transform rightFirePoint;
    public Transform leftFirePoint;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public HealthBar hpBar;
    public ScoreController scoreController;
    public DeathMenuController deathMenuController;
    
    public DateTime lastCollision = DateTime.MinValue;
    public int invulnaribilityMillis = 1500;
    public float damageExplosionRadius = 40f;

    public int maxHealth = 3;
    public int health = 2;
    public int scoresPerSecond = 10;

    public bool hasShield = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        shootSource = gameObject.GetComponent<AudioSource>();
        UpdateHealthUI();
        StartAddScore();
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

    void AddScoreWhileAlive()
    {
        scoreController.AddScore(scoresPerSecond);
    }

    void StopAddScore()
    {
        CancelInvoke(nameof(AddScoreWhileAlive));
    }

    void StartAddScore()
    {
        InvokeRepeating(nameof(AddScoreWhileAlive), 1.0f, 1.0f);
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
        shootSource.Play();
    }

    private void UpdateHealthUI()
    {
        hpBar.UpdateHealth((float) this.health / this.maxHealth);
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            // Do die logic if health <= 0
            StopAddScore();
            Time.timeScale = 0;
            SaveGame();
            deathMenuController.ToggleEndMenu();
            health = 0;
        }

        Explode(gameObject.transform.position, damageExplosionRadius);
        UpdateHealthUI();
    }

    public bool TryHeal()
    {
        if (health >= maxHealth) return false;
        health++;
        UpdateHealthUI();
        return true;
    }

    private void SaveGame()
    {
        HighScoreRepository.Instance.AddScore(new HighScore()
        {
            score = scoreController.CurrentScore,
            time = DateTime.Now
        });
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void Explode(Vector2 where, float howBig)
    {
        CameraShaker.Instance.ShakeOnce(4.0f, 4.0f, 0.1f, 1.0f);
        Instantiate(explosionPrefab, where, explosionPrefab.transform.rotation);
        var destroyableColliders = Physics2D.OverlapCircleAll(where, howBig);
        foreach (var toDestroyCollider in destroyableColliders)
        {
            var enemyController = toDestroyCollider.gameObject.GetComponent<AbstractEnemy>();
            if (toDestroyCollider.gameObject.CompareTag("Enemy") && enemyController != null)
            {
                enemyController.InitiateDestroy();
            }
        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        scoreController.HalfScore();
        health = 2;
        UpdateHealthUI();
        StartAddScore();
    }

    public void QuitGame(Boolean doSaveGame = true)
    {
        if (doSaveGame)
            SaveGame();
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }

}