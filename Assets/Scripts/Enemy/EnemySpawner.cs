﻿using System;
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
        public GameObject shieldedPrefab;
        public GameObject player;
        public ScoreController scoreController;
        private DateTime nextEnemySpawnTime = DateTime.Now;
        public DifficultyController difficultyController;
        private DifficultyController.Difficulty difficulty;
        public float spawnTimeSpanStart = 2.0f;
        public float spawnTimeSpanEnd = 5.0f; 

        // Add Group spawning support
        private Dictionary<EnemyType, float> spawnChancesEasy = new Dictionary<EnemyType, float>()
        {
            {EnemyType.Snitch, 0.5f},
            {EnemyType.Minion, 0.3f},
            {EnemyType.Walker, 0.8f},
            {EnemyType.Dodger, 0.01f},
            {EnemyType.Bumper, 0.8f},
            {EnemyType.Shielded, 0.3f}
        };
        
        private Dictionary<EnemyType, float> spawnChances = new Dictionary<EnemyType, float>()
        {
            {EnemyType.Snitch, 1.0f},
            {EnemyType.Minion, 1.0f},
            {EnemyType.Walker, 1.0f},
            {EnemyType.Dodger, 1.0f},
            {EnemyType.Bumper, 1.0f}
        };

        private void Start()
        {
            nextEnemySpawnTime = DateTime.Now + TimeSpan.FromSeconds(Random.Range(spawnTimeSpanStart, spawnTimeSpanEnd));
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
                    spawnChances[EnemyType.Snitch] = spawnChancesEasy[EnemyType.Snitch];
                    spawnChances[EnemyType.Minion] = spawnChancesEasy[EnemyType.Minion];
                    spawnChances[EnemyType.Walker] = spawnChancesEasy[EnemyType.Walker];
                    spawnChances[EnemyType.Dodger] = spawnChancesEasy[EnemyType.Dodger];
                    spawnChances[EnemyType.Bumper] = spawnChancesEasy[EnemyType.Bumper];
                    break;
                }
                case DifficultyController.Difficulty.Normal:
                {
                    spawnChances[EnemyType.Snitch] = spawnChancesEasy[EnemyType.Snitch] / 2;
                    spawnChances[EnemyType.Minion] = spawnChancesEasy[EnemyType.Minion] * 1.5f;
                    spawnChances[EnemyType.Walker] = spawnChancesEasy[EnemyType.Walker] / 1.5f;
                    spawnChances[EnemyType.Dodger] = spawnChancesEasy[EnemyType.Dodger] * 5;
                    spawnChances[EnemyType.Bumper] = spawnChancesEasy[EnemyType.Bumper] / 1.5f;
                    break;
                }
                case DifficultyController.Difficulty.Hard:
                {
                    spawnChances[EnemyType.Snitch] = spawnChancesEasy[EnemyType.Snitch] / 5;
                    spawnChances[EnemyType.Minion] = spawnChancesEasy[EnemyType.Minion] * 2.0f;
                    spawnChances[EnemyType.Walker] = spawnChancesEasy[EnemyType.Walker] / 2.0f;
                    spawnChances[EnemyType.Dodger] = spawnChancesEasy[EnemyType.Dodger] * 15;
                    spawnChances[EnemyType.Bumper] = spawnChancesEasy[EnemyType.Bumper] / 2.0f;
                    break;
                }
                case DifficultyController.Difficulty.VeryHard:
                {
                    spawnChances[EnemyType.Snitch] = spawnChancesEasy[EnemyType.Snitch] / 20;
                    spawnChances[EnemyType.Minion] = spawnChancesEasy[EnemyType.Minion] * 3.0f;
                    spawnChances[EnemyType.Walker] = spawnChancesEasy[EnemyType.Walker];
                    spawnChances[EnemyType.Dodger] = spawnChancesEasy[EnemyType.Dodger] * 50;
                    spawnChances[EnemyType.Bumper] = spawnChancesEasy[EnemyType.Bumper];
                    break;
                }
                case DifficultyController.Difficulty.Impossible:
                {
                    spawnChances[EnemyType.Snitch] = spawnChancesEasy[EnemyType.Snitch] / 50;
                    spawnChances[EnemyType.Minion] = spawnChancesEasy[EnemyType.Minion] * 5;
                    spawnChances[EnemyType.Walker] = spawnChancesEasy[EnemyType.Walker] * 2;
                    spawnChances[EnemyType.Dodger] = spawnChancesEasy[EnemyType.Dodger] * 200;
                    spawnChances[EnemyType.Bumper] = spawnChancesEasy[EnemyType.Bumper];
                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            if (DateTime.Now >= nextEnemySpawnTime)
            {
                nextEnemySpawnTime = DateTime.Now + TimeSpan.FromSeconds(Random.Range(spawnTimeSpanStart, spawnTimeSpanEnd));
                List<EnemyType> enemyTypesToSpawn = GenerateNextEnemyTypes();
                GameObject enemy = null;
                foreach (EnemyType enemyType in enemyTypesToSpawn)
                {
                    int spawnAmount = difficultyController.GetDifficulty() == DifficultyController.Difficulty.Impossible
                        ? AbstractEnemy.EnemyGroupSize(enemyType) + 1
                        : AbstractEnemy.EnemyGroupSize(enemyType);
                    for (int i = 0; i < spawnAmount; i++)
                    {
                        switch (enemyType)
                        {

                            case EnemyType.Shielded:
                            {
                                enemy  = Instantiate(shieldedPrefab, GenerateEnemyStartingPosition(),
                                    Quaternion.Euler(0, 0, 0));
                                enemy.GetComponent<ShieldedBumperController>().player = player;
                                break;
                            }
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
            return startPosition;
        }

        private List<EnemyType> GenerateNextEnemyTypes()
        {
            List<EnemyType> toReturn = new List<EnemyType>();
            foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
            {
                if (spawnChances.ContainsKey(type))
                {
                    var chance = spawnChances[type];
                    while (chance >= 1.0)
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