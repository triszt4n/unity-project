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
        public int directionChangeMillis = 1500;
        public float moveSpeed = 5f;
        public int worthIfCaught = 200;
        private new void Start()
        {
            base.Start();            
        }

        private DateTime lastChangedDirection = DateTime.Now;
        private void ChangeDirectionWhenNeeded()
        {
            if ((DateTime.Now - lastChangedDirection).TotalMilliseconds >= directionChangeMillis)
            {
                lastChangedDirection = DateTime.Now;
                var newDirection = Random.insideUnitCircle.normalized;
                direction = newDirection;
            }
        }

        private void Update()
        {
            ChangeDirectionWhenNeeded();
        }

        private void FixedUpdate()
        {
            var currentPosition = EnemyBody.position;
            EnemyBody.MovePosition(
                currentPosition + direction.normalized * moveSpeed * Time.fixedDeltaTime
            );
        }
        private new void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            
            if(!other.gameObject.CompareTag("Player")) return;
            
            var scoreController = ScoreController.Instance;
            
            scoreController.AddScore(worthIfCaught);
            Destroy(gameObject);
        }
    }
}