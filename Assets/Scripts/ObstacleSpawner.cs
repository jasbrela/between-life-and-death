using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    private const float Delay = 3.5f;

    void Start()
    {
        Invoke("SpawnObstacle", Delay);
    }

    private void SpawnObstacle()
    {
        float minRepeat = 1.5f;
        float maxRepeat = 2f;
        
        Instantiate(obstaclePrefab, transform.position, transform.rotation);

        Invoke("SpawnObstacle", Random.Range(minRepeat, maxRepeat));
    }
}
