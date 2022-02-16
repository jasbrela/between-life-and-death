using Player;
using Store;
using UnityEngine;

namespace Spawners
{
    public class CommonSpawnableController : SpawnableController
    {
        [SerializeField] private bool isCandy;
        
        protected override void Move()
        {
            if (PlayerStatus.isGameOver) return;
            if (PlayerStatus.isGhostMode && !isFromGhostMode) return;
            
            switch (index)
            {
                case 0:
                    transform.Translate(Vector3.left * minSpeed / 3.5f * Time.deltaTime);
                    transform.Translate(Vector3.down * (minSpeed * Time.deltaTime));
                    transform.localScale += Vector3.one * (Time.deltaTime * 0.08f);
                    break;
                case 1:
                    transform.Translate(Vector3.down * (minSpeed * Time.deltaTime));
                    transform.localScale += Vector3.one * (Time.deltaTime * 0.15f);
                    break;
                case 2:
                    transform.Translate(Vector3.right * minSpeed / 3.5f * Time.deltaTime);
                    transform.Translate(Vector3.down * (minSpeed * Time.deltaTime));
                    transform.localScale += Vector3.one * (Time.deltaTime * 0.08f);
                    break;
            }

            if (isCandy
                && PowerUpManager.Instance.IsCandyMagnetActive
                && Vector3.Distance(transform.position, player.position) < 3
                && transform.position.y > player.position.y)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    player.position, 10 * Time.deltaTime);
            }
        }
    }
}