using Player;
using Store;
using UnityEngine;

namespace Spawners
{
    public class Candy : BaseSpawnable
    {
        private int _index;
    
        new void Start()
        {
            base.Start();
        }
    
        public void Init(int index)
        {
            _index = index;

            transform.position = new Vector3(pos.positions[index].position.x,
                pos.positions[index].position.y, transform.position.z);
        }

        private void Update()
        {
            Move(_index);

            if (PowerUpManager.Instance.GetIsCandyMagnetActive() &&
                Vector3.Distance(transform.position, PlayerMovement.position) < 3
                && transform.position.y > PlayerMovement.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    PlayerMovement.position, 10 * Time.deltaTime);
            }
            
            FixCollider(false);
            DestroySpawnable();
        }
    }
}
