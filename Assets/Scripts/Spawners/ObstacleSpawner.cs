using Store;
using UnityEngine;

namespace Spawners
{
    public class ObstacleSpawner : BaseSpawner
    {
        private new const float Delay = 3.5f;
    
        private new void Start()
        {
            Invoke(nameof(Spawn), Delay);
        }
    
        private new void Spawn()
        {
            float minRepeat = 1.5f;
            float maxRepeat = 2f;

            if (PowerUpManager.Instance.GetIsNoMoreObstaclesActive()) return;
        
            base.Spawn();
            Invoke(nameof(Spawn), Random.Range(minRepeat, maxRepeat));
        }
    }
}