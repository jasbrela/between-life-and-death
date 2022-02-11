using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    [CreateAssetMenu(menuName = "Spawners/Spawnable")]
    public class Spawner : ScriptableObject
    {
        public Position pos;
        public GameObject prefab;
        public Sprite[] sprites;
        public float firstSpawnDelay = 5f;
        public float minRepeatDelay;
        public float maxRepeatDelay;
        public bool isCloud;

        private SpawnableController _lastSpawnedObj;
        
        public int Index { get; set; }

        public void Spawn(Transform parent, ref Transform player)
        {
            if (PlayerStatus.GameOver) return;
            
            GameObject obj = Instantiate(prefab, parent.position, parent.rotation);
            obj.GetComponent<SpawnableController>().Initialize(Index, ref player);
            obj.transform.parent = parent;
            obj.transform.position = GetInitialPosition();
            RandomizeSprite();
        }
        
        private void RandomizeSprite()
        {
            if (sprites.Length > 0)
            {
                prefab.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            }
        }
        
        private float RandomizeYPos()
        {
            return pos.positions[Index].position.y + Random.Range(-1.5f, 1.0f);
        }

        private Vector2 GetInitialPosition()
        {
            if (isCloud)
            {
                return new Vector3(pos.positions[Index].position.x,
                    RandomizeYPos(), 0f);
            }
            
            return new Vector3(pos.positions[Index].position.x,
                pos.positions[Index].position.y, 0f);
        }
    }
}
