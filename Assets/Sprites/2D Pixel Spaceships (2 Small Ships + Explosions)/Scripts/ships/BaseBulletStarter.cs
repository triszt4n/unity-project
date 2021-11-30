using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallShips
{

    public class BaseBulletStarter : MonoBehaviour {

        public GameObject bulletPrefab; 
        [Tooltip("Lest of empty child GameObjects on the ship where bullet will appear")]
        public Transform[] bulletStartPoses;
        [Tooltip("If 0 than new sortingOrder will implemented for bullet")]
        public int bulletSortingOrder = 0;
        public float bulletSpeed;
        [Tooltip("Delay between each bullet if repeat fire mode")]
        public float fireDelay;
        [Tooltip("Should bullets appear one after another or all at once. Use for ships with many bulletStartPoses")]
        public bool fireInSequence;

        [Space(20)]
        public GameObject bombPrefab;
        public Transform bombStartPos;
        public float bombSpeed;

        bool repeatFire = false;
        int fireIndex = 0;

        void OneShot(int index)
        {
            if (IfIndexGood(index))
            {
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletStartPoses[index].position, Quaternion.identity);
                if (bulletSortingOrder != 0)
                {
                    bullet.GetComponent<SpriteRenderer>().sortingOrder = bulletSortingOrder;
                }
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bulletSpeed * (-bulletStartPoses[index].up);
            }
        }

        public void LaunchBomb()
        {
            GameObject bomb = (GameObject)Instantiate(bombPrefab, bombStartPos.position, bombStartPos.rotation);
            Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            rb.velocity = bombSpeed * (-transform.up);
        }

        bool IfIndexGood(int index)
        {
            if (bulletStartPoses != null && index >= 0 && index < bulletStartPoses.Length)
            {
                return true;
            } else
            {
                Debug.LogWarning("index is out of range in bulletStartPoses");
                return false;
            }
        }

        public void StartRepeateFire()
        {
            if (!repeatFire)
            {
                repeatFire = true;
                fireIndex = 0;
                StartCoroutine(RepeateFire());
            }
        }
    
        public void StopRepeatFire()
        {
            repeatFire = false;
        }

        public void MakeOneShot()
        {
            for (int index = 0; index < bulletStartPoses.Length; index++)
                OneShot(index);
        }

        private void OnDestroy()
        {
            StopCoroutine(RepeateFire());
        }

        IEnumerator RepeateFire()
        {
            while (repeatFire)
            {
                if (fireInSequence) {
                    OneShot(fireIndex);
                    if (++fireIndex >= bulletStartPoses.Length)
                        fireIndex = 0;
                } else
                {
                    MakeOneShot();
                }
                yield return new WaitForSeconds(fireDelay);
            }
        }
    }
}
