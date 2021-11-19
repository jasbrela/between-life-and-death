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
            if (PowerUpManager.Instance.GetIsCandyMagnetActive()) return;
            Move(_index);

            FixCollider(false);
            DestroySpawnable();
        }
    }
}
