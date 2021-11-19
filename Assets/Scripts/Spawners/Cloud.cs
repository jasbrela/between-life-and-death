using UnityEngine;

namespace Spawners
{
    public class Cloud : BaseSpawnable
    {
        private int _startPosIndex;
        private float _speed;
        [SerializeField] private float maxSpeed = 1;
        [SerializeField] private float minSpeed = 2;

        private new void Start()
        {
            base.Start();
        
            _startPosIndex = Random.Range(0, pos.positions.Length);
        
            RandomizeSprite();

            transform.position = new Vector3(pos.positions[_startPosIndex].position.x,
                RandomizeYPos(), transform.position.z);

            _speed = Random.Range(maxSpeed, minSpeed);
        }

        private void Update()
        {
            Move();
            FixCollider();
            DestroySpawnable();
        }

        private void Move()
        {
            switch (_startPosIndex)
            {
                case 1:
                    transform.Translate(Vector3.left * _speed * Time.deltaTime);
                    break;
                case 0:
                    transform.Translate(Vector3.right * _speed * Time.deltaTime);
                    break;
            }
        }

        private float RandomizeYPos()
        {
            return pos.positions[_startPosIndex].position.y + Random.Range(-1.5f, 1.0f);
        }
    }
}
