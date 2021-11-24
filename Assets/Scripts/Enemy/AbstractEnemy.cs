using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class AbstractEnemy : MonoBehaviour
    {
        protected Rigidbody2D EnemyBody;
        public int worthIfShot = 50;
        public int damagePerSecond = 1;
        private DateTime lastCollisionWithPlayer = DateTime.MinValue; 
        protected void Start()
        {
            EnemyBody = gameObject.GetComponent<Rigidbody2D>();
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.gameObject.CompareTag("Projectile")) return;
            
            var scoreController = ScoreController.Instance;
            
            scoreController.AddScore(worthIfShot);
            Destroy(gameObject);
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            if(!other.collider.gameObject.CompareTag("Player")) return;
            if ((DateTime.Now - lastCollisionWithPlayer).Seconds < 1) return;
            lastCollisionWithPlayer = DateTime.Now;
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damagePerSecond);
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