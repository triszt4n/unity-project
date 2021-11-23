using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        private DateTime _nextEnemySpawnTime = DateTime.Now;

        private readonly Type[] _spawnedEnemyTypes = new Type[]
        {
            typeof(SnitchController)
        };
        public int[] spawnedEnemyWeights = new int[]
        {
            5
        }; 
        private void Start()
        {
            _nextEnemySpawnTime = DateTime.Now + TimeSpan.FromSeconds(2.0f);
        }

        private void Update()
        {
            if (DateTime.Now >= _nextEnemySpawnTime)
            {
                _nextEnemySpawnTime = DateTime.Now + TimeSpan.FromSeconds(1.0f + Random.Range(0.0f, 2.0f));
                var createdObject = Instantiate(enemyPrefab, GenerateEnemyStartingPosition(), Quaternion.Euler(0, 0, 0));
                createdObject.AddComponent(typeof(CircleCollider2D));
                createdObject.GetComponent<CircleCollider2D>().radius = 2.0f;
                createdObject.AddComponent(typeof(Rigidbody2D));
                createdObject.AddComponent(GenerateNextEnemyType());
            }
        }

        // place enemies in other places
        private Vector3 GenerateEnemyStartingPosition()
        {
            return new Vector3(0, 0, 0);
        }

        private Type GenerateNextEnemyType()
        {
            int sumWeights = spawnedEnemyWeights.Sum();
            int selectedWeight = spawnedEnemyWeights.First();
            int selectedIndex = 0;
            int randomSelection = Random.Range(0, sumWeights);
            while (selectedWeight < randomSelection && selectedIndex + 1 < _spawnedEnemyTypes.Length)
            {
                selectedWeight += spawnedEnemyWeights[selectedIndex + 1];
                selectedIndex++;
            }
            
            return _spawnedEnemyTypes[selectedIndex];
        }
    }
}