using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class ShieldedBumperController: AbstractEnemy
    {

        public float moveSpeed = 10;
        public float maxRotationSpeed = 5;
        public GameObject player;
        
        private new void Start()
        {
            base.Start();
            var dir = Random.insideUnitCircle.normalized;
            EnemyBody.velocity = dir * moveSpeed;
            var rotation = Random.insideUnitCircle;
        }
        

        private void FixedUpdate()
        {
            Vector2 lookDir = transform.position - player.transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            var targetRotation = Quaternion.Euler(0,0,angle);
            EnemyBody.MoveRotation(Quaternion.Slerp(transform.rotation,targetRotation, maxRotationSpeed * Time.fixedDeltaTime));

        }
    }
}