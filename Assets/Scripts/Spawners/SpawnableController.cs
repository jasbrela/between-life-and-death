using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Spawners
{
    public abstract class SpawnableController : MonoBehaviour
    {
        [SerializeField] private protected FloatVariable minSpeed;
        [SerializeField] private protected FloatVariable maxSpeed;

        [SerializeField] private protected bool isFromGhostMode;
        private protected Transform player;
        private protected int index;
        private protected float speed;

        private void Start()
        {
            speed = Math.Abs(minSpeed.value - maxSpeed.value) > .1f
                ? Random.Range(minSpeed.value, maxSpeed.value)
                : minSpeed.value;
            
            bool isGhostMode = PlayerStatus.isGhostMode && SceneManager.GetActiveScene().name == Scenes.Ghost.ToString();
            
            if (isFromGhostMode && !isGhostMode || !isFromGhostMode && isGhostMode || PlayerStatus.isGameOver)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            Move();
            FixCollider();
            DestroySpawnable();
        }
        
        public void Initialize(int index, ref Transform player)
        {
            this.index = index;
            this.player = player;
        }

        protected virtual void Move() { }

        private void FixCollider()
        {
            if (player == null) return;
            if (!(transform.position.y < player.position.y)) return;
            
            GetComponent<SpriteRenderer>().sortingLayerName = "SpawnableFront";
            GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f);
            GetComponent<Collider2D>().enabled = false;
        }

        private void DestroySpawnable()
        {
            if (transform.position.y < -6f || transform.position.x < -5f || transform.position.x > 5f)
            {
                Destroy(gameObject);
            }
        }
    }
}