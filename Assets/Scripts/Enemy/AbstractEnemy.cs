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
        public DebrisController debris;
        public ScoreController scoreController;

        protected void Start()
        {
            EnemyBody = gameObject.GetComponent<Rigidbody2D>();
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Projectile")) return;


           
            scoreController.AddScore(worthIfShot);
            Destroy(other.gameObject);
            InitiateDestroy();
        }

        public void InitiateDestroy()
        {
            if (debris != null)
            {
                Instantiate(debris, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        
        protected void OnCollisionEnter2D(Collision2D other)
        {
            // We don't collide if
            // * This is not a player
            if (!other.collider.gameObject.CompareTag("Player")) return;
            var playerController = other.collider.gameObject.GetComponent<PlayerController>();

            // * Somebody have recently collided with them
            // * It is a player but he has a shield
            if ((DateTime.Now - playerController.lastCollision).TotalMilliseconds <
                playerController.invulnaribilityMillis ||
                playerController.hasShield)
                return;
            playerController.lastCollision = DateTime.Now;
            playerController.TakeDamage();
        }

        public static int EnemyGroupSize(EnemyType enemyType)
        {
            int toReturn;
            switch (enemyType)
            {
                case EnemyType.Bumper:
                    toReturn = 3;
                    break;
                case EnemyType.Dodger:
                    toReturn = 4;
                    break;
                case EnemyType.Minion:
                    toReturn = 5;
                    break;
                case EnemyType.Walker:
                    toReturn = 3;
                    break;
                case EnemyType.Snake:
                    toReturn = 2;
                    break;
                case EnemyType.Shielded:
                    toReturn = 2;
                    break;
                case EnemyType.Snitch:
                    toReturn = 1;
                    break;
                default:
                    toReturn = 1;
                    break;
            }

            return toReturn;
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