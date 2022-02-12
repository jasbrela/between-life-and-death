using UnityEngine;

namespace Spawners
{
    [CreateAssetMenu(fileName = "Spawner Information")]
    public class SpawnerInformation : ScriptableObject
    {
        public Spawner cloudSpawner;
        public Spawner candySpawner;
        public Spawner obstacleSpawner;
        public Spawner soulSpawner;
        public Spawner[] powerUpSpawners;
    }
}