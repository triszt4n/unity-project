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
        public GameObject hologramPrefab;
        public GameObject shieldPrefab;
        public GameObject gatePrefab;
        public GameObject superModePrefab;
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
            }
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
                    var randomValue = Random.Range(0.0f, spawnChances[type]);
                    if ( <= spawnChances[type])
                    {
                        toReturn.Add(type);
                    }
                }
            }

            return toReturn;
        }
    }   
}