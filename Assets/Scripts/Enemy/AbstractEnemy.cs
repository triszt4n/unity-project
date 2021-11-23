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
    }
}