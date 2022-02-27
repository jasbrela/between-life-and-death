using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Player;
using Store;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private IntegerVariable candyQuantity;
        [SerializeField] private SpawnerInformation info;

        private int delay = 3;
        private int _currentPowerUp;
        private int _nextPowerUp;

        private void Start()
        {
            StartAllSpawns();
        }

        private void StartAllSpawns()
        {
            StartCoroutine(StartSpawn(info.cloudSpawner));
            StartCoroutine(StartSpawn(info.obstacleSpawner));
            StartCoroutine(StartSpawn(info.soulSpawner));

            _nextPowerUp = _currentPowerUp = Random.Range(0, info.powerUpSpawners.Length);
            StartCoroutine(StartSpawn(info.powerUpSpawners[_currentPowerUp]));

            ResetIndex(info.candySpawner);
            StartCoroutine(SpawnCandy());

        }

        private IEnumerator StartSpawn(Spawner spawner)
        {
            ResetIndex(spawner);
            yield return new WaitForSeconds(spawner.firstSpawnDelay.value);
            StartCoroutine(Spawn(spawner));
        }

        private IEnumerator Spawn(Spawner spawner)
        {
            if (PlayerStatus.isGameOver) yield break;

            while (!PlayerStatus.isGhostMode && spawner == info.soulSpawner) yield return null;

            if (spawner != info.candySpawner)
            {
                SetIndex(spawner);
                StartCoroutine(ResetIndexAfterInterval(spawner, delay));
            }
            
            Spawner temp = spawner;
            float minDelay = spawner.minRepeatDelay.value;
            float maxDelay = spawner.maxRepeatDelay.value;

            var isSpawningPowerUp = info.powerUpSpawners.Contains(spawner);

            if (PowerUpManager.Instance.IsAnyPowerUpActive() != PowerUpType.None)
            {
                if (!isSpawningPowerUp)
                {
                    spawner.Spawn(transform, ref player);
                }
                else
                {
                    minDelay = 5f;
                    maxDelay = 5f;
                }
            }
            else
            {
                spawner.Spawn(transform, ref player);
                
                if (isSpawningPowerUp)
                {
                    _currentPowerUp = _nextPowerUp;
                    
                    _nextPowerUp = _currentPowerUp + 1;
                    if (_nextPowerUp == info.powerUpSpawners.Length) _nextPowerUp = 0;
                
                    temp = info.powerUpSpawners[_nextPowerUp];
                    minDelay = temp.minRepeatDelay.value;
                    maxDelay = temp.maxRepeatDelay.value;
                }
            }

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            StartCoroutine(Spawn(temp));
        }

        private IEnumerator SpawnCandy()
        {
            yield return new WaitForSeconds(Random.Range(info.candySpawner.minRepeatDelay.value, info.candySpawner.maxRepeatDelay.value));
            
            info.candySpawner.Index = Random.Range(0, 3);

            StartCoroutine(ResetIndexAfterInterval(info.candySpawner, candyQuantity.value + delay));
            
            for (int i = 0; i < candyQuantity.value; i++)
            {
                if (i == 0 && PowerUpManager.Instance.CanCount) PowerUpManager.Instance.StartTimer();
                if (PlayerStatus.isGameOver || PlayerStatus.isGhostMode) yield break;
                yield return new WaitForSeconds(0.5f);
                info.candySpawner.Spawn(transform, ref player);
            }
            
            if (PlayerStatus.isGameOver || PlayerStatus.isGhostMode) yield break;
            
            StartCoroutine(SpawnCandy());
        }

        private void GetAvailableIndex(Spawner spawner, int[] avoidedIndexes)
        {
            if (PlayerStatus.isGameOver) return;

            spawner.Index = Random.Range(0, 3);

            bool avoid = avoidedIndexes.Any(avoidedIndex => spawner.Index == avoidedIndex);

            if (!avoid) return;
            
            switch (spawner.Index)
            {
                case 0:
                    spawner.Index += Random.Range(1, 3);
                    avoid = false;
                    break;
                case 1:
                    float chance = Random.Range(0f, 1f);
                    spawner.Index += chance > 0.5f ? 1 : -1;
                    avoid = false;
                    break;
                case 2:
                    spawner.Index -= Random.Range(1, 3);
                    avoid = false;
                    break;
            }

            if (avoid) spawner.Index = -1;
        }

        private void SetIndex(Spawner spawner)
        {
            if (spawner == info.cloudSpawner)
            {
                spawner.Index = Random.Range(0, 2);
                return;
            }
            
            List<int> avoid = new List<int>();
            
            CheckWith(spawner, info.candySpawner, avoid);
            CheckWith(spawner, info.obstacleSpawner, avoid);
            CheckWith(spawner, info.powerUpSpawners[_currentPowerUp], avoid);

            if (PlayerStatus.isGhostMode) CheckWith(spawner, info.soulSpawner, avoid);

            GetAvailableIndex(spawner, avoid.ToArray());
        }
        
        private void CheckWith(Spawner spawner, Spawner spawnerToCheck, List<int> avoid)
        {
            if (spawner == spawnerToCheck || spawnerToCheck.Index == -1) return;
            avoid.Add(spawnerToCheck.Index);
        }

        private IEnumerator ResetIndexAfterInterval(Spawner spawner, int seconds)
        {
            float count = 0f;
            float interval = .1f;
            
            while (count <= seconds)
            {
                if (PlayerStatus.isPaused || PlayerStatus.isGameOver || !Application.isFocused) yield return null;
                
                yield return new WaitForSeconds(interval);
                count += interval;
            }
            
            ResetIndex(spawner);
        }
        
        private void ResetIndex(Spawner spawner)
        {
            spawner.Index = -1;
        }
    }
}
