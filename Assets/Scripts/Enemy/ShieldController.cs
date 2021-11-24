using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class ShieldController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.gameObject.CompareTag("Projectile")) return;
            Destroy(other.gameObject);
        }
    }
}