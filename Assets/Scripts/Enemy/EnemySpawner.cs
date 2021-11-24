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
        
        // Add Group spawning support
        private Dictionary<EnemyType, double> spawnChances = new Dictionary<EnemyType, double>()
        {
            {EnemyType.Snitch, 0.5},
            {EnemyType.Minion, 0.5},
            {EnemyType.Walker, 0.5},
            {EnemyType.Dodger, 0.5},
            {EnemyType.Bumper, 0.5}
        };
        private void Start()
        {
            nextEnemySpawnTime = DateTime.Now + TimeSpan.FromSeconds(2.0f);
        }

        private void Update()
        {
            if (DateTime.Now >= nextEnemySpawnTime)
            {
                nextEnemySpawnTime = DateTime.Now + TimeSpan.FromSeconds(1.0f + Random.Range(0.0f, 2.0f));
                List<EnemyType> enemyTypesToSpawn = GenerateNextEnemyTypes();
                GameObject enemy = null;
                foreach (EnemyType enemyType in enemyTypesToSpawn)
                {
                    switch (enemyType)
                    {
                        case EnemyType.Snitch:
                        {
                            var snitch = Instantiate(snitchPrefab, GenerateEnemyStartingPosition(), Quaternion.Euler(0, 0, 0));
                            enemy = snitch;
                            snitch.GetComponent<SnitchController>().objectToRunFrom =
                                player.GetComponent<Collider2D>();
                            break;
                        }
                        case EnemyType.Minion:
                        {
                            var minion = Instantiate(minionPrefab, GenerateEnemyStartingPosition(), Quaternion.Euler(0, 0, 0));
                            enemy = minion;
                            minion.GetComponent<MinionController>().objectToChase =
                                player.GetComponent<Collider2D>();
                            break;
                        }
                        case EnemyType.Walker:
                        {
                            enemy = Instantiate(walkerPrefab, GenerateEnemyStartingPosition(), Quaternion.Euler(0, 0, 0));
                            break;
                        }
                        case EnemyType.Dodger:
                        {
                            var dodger = Instantiate(dodgerPrefab, GenerateEnemyStartingPosition(), Quaternion.Euler(0, 0, 0));
                            enemy = dodger;
                            dodger.GetComponent<DodgerController>().objectToChase =
                                player.GetComponent<Collider2D>();
                            break;
                        }
                        case EnemyType.Bumper:
                        {
                            enemy = Instantiate(bumperPrefab, GenerateEnemyStartingPosition(), Quaternion.Euler(0, 0, 0));
                            break;
                        }
                    }
                    if (enemy != null) enemy.GetComponent<AbstractEnemy>().scoreController = scoreController;
                }
            }
        }

        // place enemies in other places
        private Vector3 GenerateEnemyStartingPosition()
        {
            return Random.insideUnitSphere * 10;
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