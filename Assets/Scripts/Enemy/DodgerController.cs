using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class DodgerController : AbstractEnemy
    {
        private Vector2 direction;
        public float chaseSpeed = 5f;
        public float dodgeSpeed = 10f;
        public float dodgeInRadius = 4f;
        public Collider2D objectToChase;
        private MovementType movementType = MovementType.Chasing;
        private void Update()
        {
            movementType = projectilesNearby().Count > 0 ? MovementType.Dodging : MovementType.Chasing;
        }

        private List<Collider2D> projectilesNearby()
        {
            List<Collider2D> toReturn = new List<Collider2D>();
            var collisions = Physics2D.OverlapCircleAll(EnemyBody.position, dodgeInRadius);
            foreach (var collider in collisions)
            {
                if (collider.CompareTag("Projectile"))
                {
                    toReturn.Add(collider);
                }
            }
            return toReturn;
        }

        private void FixedUpdate()
        {
            var position = EnemyBody.position;
            if (movementType == MovementType.Chasing)
            {
                direction = objectToChase.ClosestPoint(position) - (position);
                EnemyBody.MovePosition(
                    position + direction.normalized * chaseSpeed * Time.fixedDeltaTime
                );
            }
            else
            {
                List<Collider2D> projectilesToRunFrom = projectilesNearby();
                direction = Vector2.zero;
                foreach (var collider in projectilesToRunFrom)
                {
                    direction += position - collider.ClosestPoint(position);
                }
                EnemyBody.MovePosition(
                    position + direction.normalized * dodgeSpeed * Time.fixedDeltaTime
                );
            }
        }

        private enum MovementType
        {
            Dodging,
            Chasing
        }
    }
}