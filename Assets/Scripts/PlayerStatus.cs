using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static bool GhostMode;
    public static bool GameOver;
    [SerializeField] private GameObject gameOverMessage;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        Time.timeScale = 1 + Time.deltaTime * 2;
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
