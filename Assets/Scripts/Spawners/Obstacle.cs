using Player;
using UnityEngine;

namespace Spawners
{
    public class Obstacle : BaseSpawnable
    {
        private int _startPosIndex;

        new void Start()
        {
            base.Start();
            _startPosIndex = Random.Range(0, pos.positions.Length);

            if (!PlayerStatus.GhostMode && _startPosIndex == CandySpawner.Index)
            {
                switch (_startPosIndex)
                {
                    case 0:
                        _startPosIndex += Random.Range(1, 3); // entre 1 e 2
                        break;
                    case 1:
                        var chance = Random.Range(0f, 1f);
                        _startPosIndex += chance > 0.5f ? 1 : -1;
                        break;
                    case 2:
                        _startPosIndex -= Random.Range(1, 3); // entre 1 e 2
                        break;
                }
            }
            
            RandomizeSprite();
        
            transform.position = new Vector3(pos.positions[_startPosIndex].position.x,
                pos.positions[_startPosIndex].position.y, transform.position.z);
        }
    
        void Update()
        {
            Move(_startPosIndex);

            FixCollider();

            DestroySpawnable();
        }
    }
}
