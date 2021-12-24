using Player;
using UnityEngine;

namespace Spawners
{
    public abstract class BaseSpawnable : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] protected Position pos;
        private const float Speed = 2f;
        private GameObject _player;
    
        protected void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    
        protected void RandomizeSprite()
        {
            if (sprites.Length > 0)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            }
        }
    
        protected void Move(int index)
        {
            if (!PlayerStatus.GameOver)
            {
                switch (index)
                {
                    case 0:
                        transform.Translate(Vector3.left * Speed / 3.5f * Time.deltaTime);
                        transform.Translate(Vector3.down * Speed * Time.deltaTime);
                        transform.localScale += Vector3.one * Time.deltaTime * 0.08f;
                        break;
                    case 1:
                        transform.Translate(Vector3.down * Speed * Time.deltaTime);
                        transform.localScale += Vector3.one * Time.deltaTime * 0.15f;
                        break;
                    case 2:
                        transform.Translate(Vector3.right * Speed / 3.5f * Time.deltaTime);
                        transform.Translate(Vector3.down * Speed * Time.deltaTime);
                        transform.localScale += Vector3.one * Time.deltaTime * 0.08f;
                        break;
                }
            }
        }
    
        protected void FixCollider(bool isBox = true)
        {
            if (transform.position.y < _player.transform.position.y)
            {
                GetComponent<SpriteRenderer>().sortingLayerName = "SpawnableFront";
                if (isBox)
                {
                    GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    GetComponent<CircleCollider2D>().enabled = false;
                }
            }
        }
    
        protected void DestroySpawnable()
        {
            if (transform.position.y < -6f || transform.position.x < -5f || transform.position.x > 5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
