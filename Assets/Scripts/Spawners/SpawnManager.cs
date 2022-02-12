using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private int candyQuantity;
        [SerializeField] private SpawnerInformation info;

        private int _lastPowerUp;
        
        private void Start()
        {
            StartAllSpawns();
        }

        private void StartAllSpawns()
        {
            StartCoroutine(StartSpawn(info.cloudSpawner));
            StartCoroutine(StartSpawn(info.obstacleSpawner));
            StartCoroutine(StartSpawn(info.soulSpawner));

            _lastPowerUp = Random.Range(0, info.powerUpSpawners.Length);
            StartCoroutine(StartSpawn(info.powerUpSpawners[_lastPowerUp]));

            ResetIndex(info.candySpawner);
            StartCoroutine(SpawnCandy());

        }

        private IEnumerator StartSpawn(Spawner spawner)
        {
            ResetIndex(spawner);
            yield return new WaitForSeconds(spawner.firstSpawnDelay);
            StartCoroutine(Spawn(spawner));
        }

        private IEnumerator Spawn(Spawner spawner)
        {
            if (PlayerStatus.isGameOver) yield break;

            while (!PlayerStatus.isGhostMode && spawner == info.soulSpawner) yield return null;

            if (spawner != info.candySpawner)
            {
                SetIndex(spawner);
                StartCoroutine(ResetIndexAfterInterval(spawner, 3f));
            }
            
            spawner.Spawn(transform, ref player);

            var temp = spawner;
            
            if (info.powerUpSpawners[_lastPowerUp] == spawner)
            {
                _lastPowerUp = Random.Range(0, info.powerUpSpawners.Length);
                temp = info.powerUpSpawners[_lastPowerUp];
            }

            yield return new WaitForSeconds(Random.Range(temp.minRepeatDelay, temp.maxRepeatDelay));
            StartCoroutine(Spawn(temp));
        }

        private IEnumerator SpawnCandy()
        {
            yield return new WaitForSeconds(Random.Range(info.candySpawner.minRepeatDelay, info.candySpawner.maxRepeatDelay));
            
            info.candySpawner.Index = Random.Range(0, 3);

            StartCoroutine(ResetIndexAfterInterval(info.candySpawner, candyQuantity + 2f));
            for (int i = 0; i < candyQuantity; i++)
            {
                if (PlayerStatus.isGameOver || PlayerStatus.isGhostMode) yield break;
                yield return new WaitForSeconds(1);
                info.candySpawner.Spawn(transform, ref player);
            }

            info.candySpawner.Index = -1;

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
                    break;
                case 1:
                    float chance = Random.Range(0f, 1f);
                    spawner.Index += chance > 0.5f ? 1 : -1;
                    break;
                case 2:
                    spawner.Index -= Random.Range(1, 3);
                    break;
            }
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
            if (PlayerStatus.isGhostMode) CheckWith(spawner, info.soulSpawner, avoid);

            GetAvailableIndex(spawner, avoid.ToArray());
        }
        
        private void CheckWith(Spawner spawner, Spawner spawnerToCheck, List<int> avoid)
        {
            if (spawner == spawnerToCheck || spawnerToCheck.Index == -1) return;
            avoid.Add(spawnerToCheck.Index);
        }

        private IEnumerator ResetIndexAfterInterval(Spawner spawner, float interval)
        {
            yield return new WaitForSecondsRealtime(interval);
            ResetIndex(spawner);
        }
        
        private void ResetIndex(Spawner spawner)
        {
            spawner.Index = -1;
        }
    }
}
