using Player;
using UnityEngine;

namespace Spawners
{
    public class BaseSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject prefab;
        protected const float Delay = 5f;
    
        protected void Start()
        {
            Invoke(nameof(Spawn), Delay);
        }
    
        protected void Spawn()
        {
            if (!PlayerStatus.GameOver)
            {
                GameObject spawnable = Instantiate(prefab, transform.position, transform.rotation);
                spawnable.transform.parent = transform;
            }
        }
    }
}
