using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class AbstractEnemy : MonoBehaviour
    {
        protected Rigidbody2D EnemyBody;
        public int worthIfShot = 50;
        public ScoreController scoreController;
        protected void Start()
        {
            EnemyBody = gameObject.GetComponent<Rigidbody2D>();
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.gameObject.CompareTag("Projectile")) return;

            scoreController.AddScore(worthIfShot);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            // We don't collide if
            // * This is not a player
            if(!other.collider.gameObject.CompareTag("Player")) return;
            var playerController = other.collider.gameObject.GetComponent<PlayerController>();
            
            // * Somebody have recently collided with them
            // * It is a player but he has a shield
            if ((DateTime.Now - playerController.lastCollision).TotalMilliseconds < playerController.invulnaribilityMillis ||
               playerController.hasShield)
                return;
            playerController.lastCollision = DateTime.Now;
            playerController.TakeDamage();
        }

        public static int EnemyGroupSize (EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Bumper: return 3;
                case EnemyType.Dodger: return 4;
                case EnemyType.Minion: return 5;
                case EnemyType.Walker: return 3;
                case EnemyType.Snake: return 2;
                case EnemyType.Shielded: return 2;
                case EnemyType.Snitch: return 1;
                default: return 1;
            }
        }
    }

    public enum EnemyType
    {
        Snitch,
        Snake,
        Shielded,
        Minion,
        Bumper,
        Walker,
        Dodger
    }
}