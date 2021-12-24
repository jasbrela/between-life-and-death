using UnityEngine;

namespace Spawners
{
    public class CloudSpawner : BaseSpawner
    {
        protected new const float Delay = 3f;

        private new void Start()
        {
            Invoke(nameof(Spawn), Delay);
        }
    
        private new void Spawn()
        {
            float minRepeat = 2.5f;
            float maxRepeat = 4f;

            base.Spawn();

            Invoke(nameof(Spawn), Random.Range(minRepeat, maxRepeat));
        }
    }
}
