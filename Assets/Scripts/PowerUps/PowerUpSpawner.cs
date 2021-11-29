using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PowerUps
{
    public class PowerUpSpawner : MonoBehaviour
    {

        public GameObject healthPrefab;
        public GameObject hologramPickupPrefab;
        public GameObject shieldPrefab;
        public GameObject gatePrefab;
        public GameObject superModePrefab;
        public DifficultyController difficultyController;
        public float spawnTimeSpanStart = 2.0f;
        public float spawnTimeSpanEnd = 5.0f;
        private DateTime nextSpawnTime = DateTime.Now;
        private Camera cam;
        public int aliveTimeEasyMillis = 60_000;
        private int aliveTimeMillis = 0;

        // Add Group spawning support
        private Dictionary<PowerUpType, double> spawnChances = new Dictionary<PowerUpType, double>()
        {
            {PowerUpType.Gate, 1.5},
            {PowerUpType.Health, 0.2},
            {PowerUpType.Hologram, 0.5},
            {PowerUpType.Shield, 0.7},
            {PowerUpType.SuperMode, 0.1}
        };

        private void UpdateAliveTime()
        {
            switch (difficultyController.GetDifficulty())
            {
                case DifficultyController.Difficulty.Easy:
                    aliveTimeMillis = aliveTimeEasyMillis;
                    break;
                case DifficultyController.Difficulty.Normal:
                    aliveTimeMillis = (int) Math.Round(aliveTimeEasyMillis * 0.85);
                    break;
                case DifficultyController.Difficulty.Hard:
                    aliveTimeMillis = (int) Math.Round(aliveTimeEasyMillis * 0.75);
                    break;
                case DifficultyController.Difficulty.VeryHard:
                    aliveTimeMillis = (int) Math.Round(aliveTimeEasyMillis * 0.65);
                    break;
                case DifficultyController.Difficulty.Impossible:
                    aliveTimeMillis = (int) Math.Round(aliveTimeEasyMillis * 0.5);
                    break;
                default: 
                    aliveTimeMillis = aliveTimeEasyMillis;
                    break;
            }
        }
        
        private void Start()
        {
            nextSpawnTime = DateTime.Now + TimeSpan.FromSeconds(Random.Range(spawnTimeSpanStart, spawnTimeSpanEnd));
            cam = Camera.main;
            UpdateAliveTime();
        }

        private void Update()
        {
            UpdateAliveTime();
            if (DateTime.Now >= nextSpawnTime)
            {
                nextSpawnTime =
                    DateTime.Now + TimeSpan.FromSeconds(Random.Range(spawnTimeSpanStart, spawnTimeSpanEnd));
                var toGenerate = GenerateNextPowerUpTypes();
                foreach (PowerUpType powerUpType in toGenerate)
                {
                    GameObject powerUp = null;
                    switch (powerUpType)
                    {
                        case PowerUpType.Gate:
                            powerUp = Instantiate(gatePrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            break;
                        case PowerUpType.Health:
                            powerUp = Instantiate(healthPrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            break;
                        case PowerUpType.Hologram:
                            powerUp = Instantiate(hologramPickupPrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            break;
                        case PowerUpType.Shield:
                            powerUp = Instantiate(shieldPrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            break;
                        case PowerUpType.SuperMode:
                            powerUp = Instantiate(superModePrefab, GeneratePowerUpStartingPosition(), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
                            break;
                    }
                    Destroy(powerUp, aliveTimeMillis / 1000.0f);
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
            Vector2 startPosition = new Vector2(Random.Range(-55, 55), Random.Range(-25, 25));
            while (CameraContainsPoint(cam, startPosition))
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
        
        private List<PowerUpType> GenerateNextPowerUpTypes()
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