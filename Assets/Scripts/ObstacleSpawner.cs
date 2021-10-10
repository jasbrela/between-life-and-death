using UnityEngine;

public class ObstacleSpawner : BaseSpawner
{
    private new const float Delay = 3.5f;

    private StoreManager _storeManager;

    private new void Start()
    {
        _storeManager = GameObject.FindWithTag("storeManager").GetComponent<StoreManager>();
        Invoke(nameof(Spawn), Delay);
    }
    
    private new void Spawn()
    {
        float minRepeat = 1.5f;
        float maxRepeat = 2f;
        
        if (_storeManager.item != 10) // se nao usa o NoObstacles
        {
            base.Spawn();
        }
        Invoke(nameof(Spawn), Random.Range(minRepeat, maxRepeat));
    }
}
