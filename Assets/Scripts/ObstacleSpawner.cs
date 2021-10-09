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

        if (!LoseCondition.GameOver)
        {
            GameObject obstacle = Instantiate(obstaclePrefab, transform.position, transform.rotation);
            obstacle.transform.parent = transform;
        }

        Invoke("SpawnObstacle", Random.Range(minRepeat, maxRepeat));
    }
}
