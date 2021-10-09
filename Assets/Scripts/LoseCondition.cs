using System;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    public static bool GameOver;
    [SerializeField] private GameObject gameOverMessage;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameOverMessage.SetActive(true);
            GameOver = true;
            Time.timeScale = 0;
        }
    }
}
