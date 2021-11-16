using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    private class PlayerState
    {
        public Vector3 position;
        public Quaternion rotation;

        public PlayerState(Transform transform)
        {
            position = transform.position;
            rotation = transform.rotation;
        }
    }

    public GameObject player;
    public Transform rightFirePoint;
    public Transform leftFirePoint;
    public GameObject bulletPrefab;
    public float followTime = 1f;
    public float shootInterval = 0.8f;

    private Queue<PlayerState> playerStates;

    private bool isLoading;

    // Start is called before the first frame update
    void Start()
    {
        playerStates = new Queue<PlayerState>();
        StartCoroutine(LoadPositions(followTime));
        InvokeRepeating(nameof(Shoot), followTime, shootInterval);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        playerStates.Enqueue(new PlayerState(player.transform));

        if (!isLoading && playerStates.Count > 0)
        {
            var state = playerStates.Dequeue();
            transform.position = state.position;
            transform.rotation = state.rotation;
        }
    }


    private void Shoot()
    {
        var liftVector = new Vector3(0, 0, 0.1f);
        Instantiate(bulletPrefab, leftFirePoint.position + liftVector, leftFirePoint.rotation);
        Instantiate(bulletPrefab, rightFirePoint.position + liftVector, rightFirePoint.rotation);
    }

    IEnumerator LoadPositions(float time)
    {
        isLoading = true;
        yield return new WaitForSeconds(time);
        isLoading = false;
    }
}