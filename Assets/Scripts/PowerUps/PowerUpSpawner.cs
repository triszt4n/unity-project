using System;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PowerUps
{
    public class PowerUpSpawner : MonoBehaviour
    {

        public GameObject healthPrefab;
        public GameObject hologramPickupPrefab;
        public GameObject hologramShipPrefab;
        public GameObject shieldPrefab;
        public GameObject gatePrefab;
        public GameObject superModePrefab;
        public GameObject explosionPrefab;
        public float spawnTimeSpanStart = 2.0f;
        public float spawnTimeSpanEnd = 5.0f;
        private DateTime nextSpawnTime = DateTime.Now;

        // Add Group spawning support
        private Dictionary<PowerUpType, double> spawnChances = new Dictionary<PowerUpType, double>()
        {
            {PowerUpType.Gate, 1.5},
            {PowerUpType.Health, 0.2},
            {PowerUpType.Hologram, 0.5},
            {PowerUpType.Shield, 0.7},
            {PowerUpType.SuperMode, 0.1}
        };
        
        private void Start()
        {
            nextSpawnTime = DateTime.Now + TimeSpan.FromSeconds(Random.Range(spawnTimeSpanStart, spawnTimeSpanEnd));
        }

        private void Update()
        {

            if (DateTime.Now >= nextSpawnTime)
            {
                nextSpawnTime =
                    DateTime.Now + TimeSpan.FromSeconds(Random.Range(spawnTimeSpanStart, spawnTimeSpanEnd));
                var toGenerate = GenerateNextEnemyTypes();
                GameObject powerUp = null;
                foreach (PowerUpType powerUpType in toGenerate)
                {
                    switch (powerUpType)
                    {
                        case PowerUpType.Gate:
                            powerUp = Instantiate(gatePrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            powerUp.GetComponent<GateController>().explosionPrefab = explosionPrefab;
                            break;
                        case PowerUpType.Health:
                            Instantiate(healthPrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            break;
                        case PowerUpType.Hologram:
                            powerUp = Instantiate(hologramPickupPrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            powerUp.GetComponent<HologramPowerup>().hologramPrefab = hologramShipPrefab;
                            break;
                        case PowerUpType.Shield:
                            Instantiate(shieldPrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            break;
                        case PowerUpType.SuperMode:
                            Instantiate(superModePrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            break;
                    }
                }
            }
        }

        private bool CameraContainsPoint(Camera c, Vector2 v)
        {
            var viewPointPoint = c.WorldToViewportPoint(v);
            return -0.1 <= viewPointPoint.x &&
                   viewPointPoint.x <= 1.1 &&
                   -0.1 <= viewPointPoint.y &&
                   viewPointPoint.y <= 1.1;
        }
        
        private Vector2 GeneratePowerUpStartingPosition()
        {
            var camera = Camera.main;
            Vector2 startPosition = new Vector2(Random.Range(-55, 55), Random.Range(-25, 25));
            while (CameraContainsPoint(camera, startPosition))
            {
                startPosition = new Vector2(Random.Range(-55, 55), Random.Range(-25, 25));
            }
            return startPosition;
        }

        enum PowerUpType
        {
            Health,
            Hologram,
            Shield,
            Gate,
            SuperMode
        }
        
        private List<PowerUpType> GenerateNextEnemyTypes()
        {
            List<PowerUpType> toReturn = new List<PowerUpType>();
            foreach (PowerUpType type in Enum.GetValues(typeof(PowerUpType)))
            {
                if (spawnChances.ContainsKey(type))
                {
                    var chance = spawnChances[type];
                    while (chance >= 1.0f)
                    {
                        toReturn.Add(type);
                        chance--;
                    }
                    if (Random.value <= chance)
                    {
                        toReturn.Add(type);
                    }
                }
            }

            return toReturn;
        }
    }   
}