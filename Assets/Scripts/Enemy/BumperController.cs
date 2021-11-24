using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class BumperController : AbstractEnemy
    {
        public float moveSpeed = 5f;

        private new void Start()
        {
            base.Start();
            gameObject.GetComponent<Rigidbody2D>().velocity =
                Random.insideUnitCircle.normalized * moveSpeed;
        }
    }
}