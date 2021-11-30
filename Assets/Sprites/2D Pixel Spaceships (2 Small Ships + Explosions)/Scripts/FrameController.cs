using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallShips
{
    public class FrameController : BaseFrameController {

        public GameObject parentShip;

        GameObject shipClone;
        
        
        bool fold = true;

        private void Start()
        {
            Init();
        }

        override public void TurnOff()
        {
            base.TurnOff();
            fold = true;
            if (parentShip != null && !IfShip7())
                parentShip.GetComponent<BaseBulletStarter>().StopRepeatFire();
        }

        public void OneShot()
        {
            if (parentShip != null)
            {
                if (IfShip7())
                {
                    parentShip.transform.Find("TurretLeft").GetComponent<BaseBulletStarter>().MakeOneShot();
                    parentShip.transform.Find("TurretRight").GetComponent<BaseBulletStarter>().MakeOneShot();
                } else {
                    parentShip.GetComponent<BaseBulletStarter>().MakeOneShot();
                }
            }
        }

        bool IfShip7()
        {
            return parentShip.name.Contains("Ship7");
        }

        public void RepeatFire()
        {
            repeatFire = !repeatFire;
            if (parentShip != null)
            {
                if (repeatFire)
                {
                    if (IfShip7())
                    {
                        parentShip.transform.Find("TurretLeft").GetComponent<BaseBulletStarter>().StartRepeateFire();
                        parentShip.transform.Find("TurretRight").GetComponent<BaseBulletStarter>().StartRepeateFire();
                    } else
                    {
                        parentShip.GetComponent<BaseBulletStarter>().StartRepeateFire();
                    }
                }
                else
                {
                    if (IfShip7())
                    {
                        parentShip.transform.Find("TurretLeft").GetComponent<BaseBulletStarter>().StopRepeatFire();
                        parentShip.transform.Find("TurretRight").GetComponent<BaseBulletStarter>().StopRepeatFire();
                    }
                    else
                    {
                        parentShip.GetComponent<BaseBulletStarter>().StopRepeatFire();
                    }
                }
            }
        }

        public void MakeDestroy()
        {
            if (parentShip != null)
            {
                parentShip.GetComponent<ExplosionController>().StartExplosion();
                shipClone = (GameObject)Instantiate(parentShip, parentShip.transform.position, Quaternion.identity);
                shipClone.SetActive(false);
                StartCoroutine(CheckShipDestroy());
            }
        }

        public void UnfoldFold()
        {
            if (parentShip != null)
            {
                fold = !fold;
                parentShip.GetComponent<Animator>().SetBool("fold", fold);
            }
        }

        public void SetAnimationTrigger(string trigger)
        {
            if (parentShip != null)
            {
                parentShip.GetComponent<Animator>().SetTrigger(trigger);
            }
        }

        IEnumerator CheckShipDestroy()
        {
            while (parentShip != null)
                yield return new WaitForSeconds(0.1f);

            Invoke("MakeCloneVisible", 2.0f);
        }

        void MakeCloneVisible()
        {
            parentShip = shipClone;
            shipClone = null;
            parentShip.SetActive(true);
        }

    }

}
