using Player;
using Spawners;
using UnityEngine;

public class SoulSpawner : BaseSpawner
{
    private new const float Delay = 25f;

    private new void Start()
    {
        Invoke(nameof(Spawn), Delay);
}

    private void Update()
    {
        if (!PlayerStatus.GhostMode)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private new void Spawn()
    {
        float minRepeat = 10f;
        float maxRepeat = 15f;
        base.Spawn();
        Invoke(nameof(Spawn), Random.Range(minRepeat, maxRepeat));
    }
}
