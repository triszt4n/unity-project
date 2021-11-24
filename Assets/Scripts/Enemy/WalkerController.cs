using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class WalkerController : AbstractEnemy
    {
        
        private Vector2 direction;
        public int directionChangeMillis = 1500;
        public float moveSpeed = 5f;

        private DateTime lastChangedDirection = DateTime.MinValue;
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
    }
}