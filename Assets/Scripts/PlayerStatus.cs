using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static Debuff CurrentDebuff;
    public static bool GhostMode;
    public static bool GameOver;
    [SerializeField] private GameObject gameOverMessage;
    private float _currentTimeScale = 1;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (CurrentDebuff != Debuff.HigherVelocity)
        {
            Time.timeScale = _currentTimeScale;    // fiz essa bobeirage pq queria que voltasse
            Time.timeScale += Time.deltaTime / 100;
            _currentTimeScale = Time.timeScale;    // o tempo ao normal dps do debuff de higher velocity
        }
        else
        {
            Time.timeScale += Time.deltaTime / 50;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (GhostMode)
            {
                gameOverMessage.SetActive(true);
                GameOver = true;
                Time.timeScale = 0;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                GhostMode = true;
                // ativa modo ghost
            }
        }
    }
}
