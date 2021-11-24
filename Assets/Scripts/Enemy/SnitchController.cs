using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SnitchController : AbstractEnemy
    {
        private Vector2 direction;
        public float moveSpeed = 5f;
        public int worthIfCaught = 200;
        public Collider2D objectToRunFrom;
        private new void Start()
        {
            base.Start();     
            
        }

        private void Update()
        {
            var position = EnemyBody.position;
            direction = position - objectToRunFrom.ClosestPoint(position);
        }

        private void FixedUpdate()
        {
            var currentPosition = EnemyBody.position;
            EnemyBody.MovePosition(
                currentPosition + direction.normalized * moveSpeed * Time.fixedDeltaTime
            );
        }

        private new void OnCollisionEnter2D(Collision2D other)
        {
            if(!other.collider.gameObject.CompareTag("Player")) return;
            
            var scoreController = ScoreController.Instance;
            
            scoreController.AddScore(worthIfCaught);
            Destroy(gameObject);
        }
    }
}