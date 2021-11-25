using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject snitchPrefab;
        public GameObject minionPrefab;
        public GameObject walkerPrefab;
        public GameObject bumperPrefab;
        public GameObject dodgerPrefab;
        public GameObject player;
        public ScoreController scoreController;
        private DateTime nextEnemySpawnTime = DateTime.Now;
        public DifficultyController difficultyController;
        private DifficultyController.Difficulty difficulty;
        
        // Add Group spawning support
        private Dictionary<EnemyType, double> spawnChances = new Dictionary<EnemyType, double>()
        {
            {EnemyType.Snitch, 0.3},
            {EnemyType.Minion, 0.3},
            {EnemyType.Walker, 0.3},
            {EnemyType.Dodger, 0.3},
            {EnemyType.Bumper, 0.3}
        };

        private void Start()
        {
            nextEnemySpawnTime = DateTime.Now + TimeSpan.FromSeconds(2.0f);
            difficulty = difficultyController.GetDifficulty();
            UpdateSpawnChancesForDifficulty();
        }

        private void Update()
        {
            UpdateSpawnChancesForDifficulty();
        }

        private void UpdateSpawnChancesForDifficulty()
        {
            // We don't touch the spawn chances if the difficulty hasn't changed
            if (difficulty == difficultyController.GetDifficulty())
            {
                return;
            }
            difficulty = difficultyController.GetDifficulty();

            switch (difficultyController.GetDifficulty())
            {
                case DifficultyController.Difficulty.Easy:
                {
                    spawnChances[EnemyType.Snitch] = 0.8;
                    spawnChances[EnemyType.Minion] = 0.6;
                    spawnChances[EnemyType.Walker] = 0.2;
                    spawnChances[EnemyType.Dodger] = 0.01;
                    spawnChances[EnemyType.Bumper] = 0.8;
                    break;
                }
                case DifficultyController.Difficulty.Normal:
                {
                    spawnChances[EnemyType.Snitch] = 0.4;
                    spawnChances[EnemyType.Minion] = 0.4;
                    spawnChances[EnemyType.Walker] = 0.4;
                    spawnChances[EnemyType.Dodger] = 0.05;
                    spawnChances[EnemyType.Bumper] = 0.5;
                    break;
                }
                case DifficultyController.Difficulty.Hard:
                {
                    spawnChances[EnemyType.Snitch] = 0.2;
                    spawnChances[EnemyType.Minion] = 0.4;
                    spawnChances[EnemyType.Walker] = 0.4;
                    spawnChances[EnemyType.Dodger] = 0.2;
                    spawnChances[EnemyType.Bumper] = 0.2;
                    break;
                }
                case DifficultyController.Difficulty.VeryHard:
                {
                    spawnChances[EnemyType.Snitch] = 0.1;
                    spawnChances[EnemyType.Minion] = 0.8;
                    spawnChances[EnemyType.Walker] = 0.6;
                    spawnChances[EnemyType.Dodger] = 0.5;
                    spawnChances[EnemyType.Bumper] = 0.4;
                    break;
                }
                case DifficultyController.Difficulty.Impossible:
                {
                    spawnChances[EnemyType.Snitch] = 0.01;
                    spawnChances[EnemyType.Minion] = 1;
                    spawnChances[EnemyType.Walker] = 1;
                    spawnChances[EnemyType.Dodger] = 1;
                    spawnChances[EnemyType.Bumper] = 1;
                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            if (DateTime.Now >= nextEnemySpawnTime)
            {
                nextEnemySpawnTime = DateTime.Now + TimeSpan.FromSeconds(1.0f + Random.Range(0.0f, 2.0f));
                List<EnemyType> enemyTypesToSpawn = GenerateNextEnemyTypes();
                GameObject enemy = null;
                foreach (EnemyType enemyType in enemyTypesToSpawn)
                {
                    for (int i = 0; i < AbstractEnemy.EnemyGroupSize(enemyType); i++)
                    {
                        switch (enemyType)
                        {
                            case EnemyType.Snitch:
                            {
                                var snitch = Instantiate(snitchPrefab, GenerateEnemyStartingPosition(),
                                    Quaternion.Euler(0, 0, 0));
                                enemy = snitch;
                                snitch.GetComponent<SnitchController>().objectToRunFrom =
                                    player.GetComponent<Collider2D>();
                                break;
                            }
                            case EnemyType.Minion:
                            {
                                var minion = Instantiate(minionPrefab, GenerateEnemyStartingPosition(),
                                    Quaternion.Euler(0, 0, 0));
                                enemy = minion;
                                minion.GetComponent<MinionController>().objectToChase =
                                    player.GetComponent<Collider2D>();
                                break;
                            }
                            case EnemyType.Walker:
                            {
                                enemy = Instantiate(walkerPrefab, GenerateEnemyStartingPosition(),
                                    Quaternion.Euler(0, 0, 0));
                                break;
                            }
                            case EnemyType.Dodger:
                            {
                                var dodger = Instantiate(dodgerPrefab, GenerateEnemyStartingPosition(),
                                    Quaternion.Euler(0, 0, 0));
                                enemy = dodger;
                                dodger.GetComponent<DodgerController>().objectToChase =
                                    player.GetComponent<Collider2D>();
                                break;
                            }
                            case EnemyType.Bumper:
                            {
                                enemy = Instantiate(bumperPrefab, GenerateEnemyStartingPosition(),
                                    Quaternion.Euler(0, 0, 0));
                                break;
                            }
                        }

                        if (enemy != null) enemy.GetComponent<AbstractEnemy>().scoreController = scoreController;
                    }
                }
            }
        }

        private bool CameraContainsPoint(Camera c, Vector2 v)
        {
            var viewPointPoint = c.WorldToViewportPoint(v);
            // 0.1 margin, so we don't see the corners of the spawned enemies either
            return -0.1 <= viewPointPoint.x &&
                   viewPointPoint.x <= 1.1 &&
                   -0.1 <= viewPointPoint.y &&
                   viewPointPoint.y <= 1.1;
        }
        
        // place enemies in other places
        private Vector2 GenerateEnemyStartingPosition()
        {
            var camera = Camera.main;
            // Removed 1 from the border, so they don't accidentally get pushed outside the border
            // when spawning
            Vector2 startPosition = new Vector2(Random.Range(-59, 59), Random.Range(-29, 29));
            while (CameraContainsPoint(camera, startPosition))
            {
                startPosition = new Vector2(Random.Range(-59, 59), Random.Range(-29, 29));
            }
            Debug.Log("Generating start position:" + startPosition);
            return startPosition;
        }

        private List<EnemyType> GenerateNextEnemyTypes()
        {
            List<EnemyType> toReturn = new List<EnemyType>();
            foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
            {
                if (spawnChances.ContainsKey(type))
                {
                    if (Random.value <= spawnChances[type])
                    {
                        toReturn.Add(type);
                    }
                }
            }

            return toReturn;
        }
    }
}