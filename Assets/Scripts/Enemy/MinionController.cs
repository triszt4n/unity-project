using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class MinionController : AbstractEnemy
    {
        private Vector2 direction;
        public float moveSpeed = 5f;
        public float maxSpeed = 12f;
        public Collider2D objectToChase;

        private new void Start()
        {
            base.Start();
        }

        private void FixedUpdate()
        {
            direction = objectToChase.ClosestPoint(EnemyBody.position) - EnemyBody.position;
            EnemyBody.AddForce(
                direction.normalized * moveSpeed
            );
            // max speed
            if (EnemyBody.velocity.magnitude > maxSpeed)
            {
                EnemyBody.velocity = EnemyBody.velocity.normalized * maxSpeed;
            }
        }
    }
}