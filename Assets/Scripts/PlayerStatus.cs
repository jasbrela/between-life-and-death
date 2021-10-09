using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public static bool GhostMode;
    public static bool GameOver;
    [SerializeField] private GameObject gameOverMessage;
    [Header("Audio")]
    [SerializeField] private AudioSource playeraudioSource;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioSource bgaudioSource;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        Time.timeScale += Time.deltaTime / 100;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (GhostMode)
            {
                bgaudioSource.Stop();
                gameOverMessage.SetActive(true);
                GameOver = true;
                Time.timeScale = 0;
                
            }
            else
            {
                SceneManager.LoadScene("Ghost");
                playeraudioSource.clip = hit;
                playeraudioSource.Play();
                GetComponent<SpriteRenderer>().color = Color.blue;
                GhostMode = true;
                // ativa modo ghost
            }
        }
    }
}
