using UnityEngine;

public class CandySpawner : BaseSpawner
{
    private int index;

    new void Start()
    {
        Invoke(nameof(SpawnCandyBlock), Delay);
    }

    private void Update()
    {
        if (PlayerStatus.GhostMode)
        {
            gameObject.SetActive(false);
        }
    }

    private void SpawnCandyBlock()
    {
        float minRepeat = 7f;
        float maxRepeat = 10f;
        
        if (!PlayerStatus.GameOver)
        {
            SpawnCandies(5);
        }
        
        Invoke(nameof(SpawnCandyBlock), Random.Range(minRepeat, maxRepeat));
    }
    
    protected new void Spawn()
    {
        if (!PlayerStatus.GameOver)
        {
            GameObject spawnable = Instantiate(prefab, transform.position, transform.rotation);
            spawnable.transform.parent = transform;
            
            spawnable.GetComponent<Candy>().Init(index);
        }
    }

    private void SpawnCandies(int quantity)
    {
        index = Random.Range(0, 3);
        for (int c = 0; c < quantity; c++)
        {
            Invoke(nameof(Spawn), c);
        }
    }
}
