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
        public float panicRadius = 10f;
        public float randomDirectionChangeMillis = 1500;
        private new void Start()
        {
            base.Start();     
            
        }

        private DateTime directionLastGenerated = DateTime.MinValue; 
        private void GenerateRandomDirectionWhenNecessary()
        {
            if ((DateTime.Now - directionLastGenerated).TotalMilliseconds >= randomDirectionChangeMillis)
            {
                directionLastGenerated = DateTime.Now;
                direction = Random.insideUnitCircle;
            }
        }

        private void Update()
        {
            var position = EnemyBody.position;
            Debug.Log("Distance from chasing " + objectToRunFrom.Distance(gameObject.GetComponent<Collider2D>()).distance);
            if (objectToRunFrom.Distance(gameObject.GetComponent<Collider2D>()).distance <= panicRadius)
            {
                direction = position - objectToRunFrom.ClosestPoint(position);          
            }
            else
            {      
                GenerateRandomDirectionWhenNecessary();
            }
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