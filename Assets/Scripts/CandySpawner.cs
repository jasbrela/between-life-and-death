using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CandySpawner : MonoBehaviour
{
    [SerializeField] private GameObject candyPrefab;
    private const float Delay = 5f;
    List<GameObject> _candies = new List<GameObject>();
    private int _index;


    void Start()
    {
        Invoke(nameof(SpawnCandyBlock), Delay);
    }

    private void SpawnCandyBlock()
    {
        float minRepeat = 7f;
        float maxRepeat = 10f;
        
        if (!PlayerStatus.GameOver)
        {
            SpawnCandies(10);
        }
        
        Invoke(nameof(SpawnCandyBlock), Random.Range(minRepeat, maxRepeat));
    }

    private void SpawnCandies(int quantity)
    {
        _index = Random.Range(0, 3);
        for (int c = 0; c < quantity; c++)
        {
            Invoke(nameof(SpawnCandy), c/2);
        }
    }

    private void SpawnCandy()
    {
        GameObject candy = Instantiate(candyPrefab, transform.position, transform.rotation);
        candy.GetComponent<Candy>().Init(_index);
        candy.transform.parent = transform;
    }
}
