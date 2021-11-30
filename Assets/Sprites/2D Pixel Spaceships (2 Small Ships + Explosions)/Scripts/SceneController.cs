using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SmallShips
{
    public class SceneController : MonoBehaviour {

        public GameObject[] arrayShips;

        BaseFrameController selectedFrame = null;

	    // Use this for initialization
	    void Start () {
            Vector2 leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector2 rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, (float)Screen.height));
            Vector2 leftTop = new Vector2(leftBottom.x, rightTop.y);
            Vector2 rightBottom = new Vector2(rightTop.x, leftBottom.y);

            EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
            collider.points = new Vector2[] { leftBottom, leftTop, rightTop, rightBottom, leftBottom };
	    }
	
        Vector2 GetMousePos()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        void CreateShip(GameObject ship)
        {
            GameObject go = (GameObject)Instantiate(ship, GetMousePos(), Quaternion.identity);
            BoxCollider2D collider = go.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
        }

        void TryToDestroy()
        {
            RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero);
            if (hit.collider != null)
            {
                ExplosionController explContr = hit.collider.GetComponent<ExplosionController>();
                if (explContr != null)
                    explContr.StartExplosion();
            }
        }

        // Update is called once per frame
        void Update () {
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.collider != null)
                {
                    if (selectedFrame != null)
                    {
                        selectedFrame.TurnOff();
                    }
                    selectedFrame = hit.collider.GetComponent<BaseFrameController>();
                    selectedFrame.TurnOn();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
            }
            
        }
    }

}
