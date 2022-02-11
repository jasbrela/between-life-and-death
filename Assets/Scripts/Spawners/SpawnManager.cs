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

        [Header("SpawnCloud Information - Please assign the references correctly")]
        [SerializeField] private Spawner cloudSpawner;
        [SerializeField] private Spawner candySpawner;
        [SerializeField] private Spawner obstacleSpawner;
        [SerializeField] private Spawner soulSpawner;

        private void Start()
        {
            StartAllSpawns();
        }

        private void StartAllSpawns()
        {
            StartCoroutine(StartSpawn(cloudSpawner));
            StartCoroutine(StartSpawn(obstacleSpawner));
            StartCoroutine(StartSpawn(soulSpawner));
            
            ResetIndex(candySpawner);
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
            if (PlayerStatus.GameOver) yield break;

            while (!PlayerStatus.GhostMode && spawner == soulSpawner) yield return null;

            if (spawner != candySpawner)
            {
                SetIndex(spawner);
                StartCoroutine(ResetIndexAfterInterval(spawner, 3f));
            }
            
            spawner.Spawn(transform, ref player);
            
            yield return new WaitForSeconds(Random.Range(spawner.minRepeatDelay, spawner.maxRepeatDelay));
            
            StartCoroutine(Spawn(spawner));

        }

        private IEnumerator SpawnCandy()
        {
            yield return new WaitForSeconds(Random.Range(candySpawner.minRepeatDelay, candySpawner.maxRepeatDelay));
            
            candySpawner.Index = Random.Range(0, 3);

            StartCoroutine(ResetIndexAfterInterval(candySpawner, candyQuantity + 2f));
            for (int i = 0; i < candyQuantity; i++)
            {
                if (PlayerStatus.GameOver || PlayerStatus.GhostMode) yield break;
                yield return new WaitForSeconds(1);
                candySpawner.Spawn(transform, ref player);
            }

            candySpawner.Index = -1;

            if (PlayerStatus.GameOver || PlayerStatus.GhostMode) yield break;
            
            StartCoroutine(SpawnCandy());
        }

        private void GetAvailableIndex(Spawner spawner, int[] avoidedIndexes)
        {
            if (PlayerStatus.GameOver) return;

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
            if (spawner == cloudSpawner)
            {
                spawner.Index = Random.Range(0, 2);
                return;
            }
            
            List<int> avoid = new List<int>();
            
            CheckWith(spawner, candySpawner, avoid);
            CheckWith(spawner, obstacleSpawner, avoid);
            if (PlayerStatus.GhostMode) CheckWith(spawner, soulSpawner, avoid);

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
