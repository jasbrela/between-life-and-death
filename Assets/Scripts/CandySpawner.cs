using UnityEngine;

public class CandySpawner : BaseSpawner
{
    private int _index;
    private const int Quantity = 5;

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
            _index = Random.Range(0, 3);
            for (int c = 0; c < Quantity; c++)
            {
                Invoke(nameof(Spawn), c);
            }
        }
        
        Invoke(nameof(SpawnCandyBlock), Random.Range(minRepeat, maxRepeat));
    }
    
    protected new void Spawn()
    {
        if (!PlayerStatus.GameOver)
        {
            GameObject spawnable = Instantiate(prefab, transform.position, transform.rotation);
            spawnable.transform.parent = transform;
            
            spawnable.GetComponent<Candy>().Init(_index);
        }
    }
}
