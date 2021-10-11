using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public static Debuff CurrentDebuff;
    public static bool GhostMode;
    public static bool GameOver;
    [SerializeField] private GameObject gameOverMessage;
    [Header("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioSource bgAudioSource;


    private void Awake()
    {
        if (!GhostMode)
        {
            Time.timeScale = 1;
            CurrentDebuff = Debuff.None;
            GameOver = false;
        }
    }

    private void Update()
    {
        if (CurrentDebuff != Debuff.HigherVelocity)
        {
            Time.timeScale += Time.deltaTime / 80;
        }
        else
        {
            Time.timeScale += Time.deltaTime / 60;
        }

        if (GameOver)
        {
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (GhostMode)
            {
                playerAudioSource.clip = hit;
                playerAudioSource.Play();
                bgAudioSource.Stop();
                if (gameOverMessage!= null) gameOverMessage.SetActive(true);
                GameOver = true;
                StartCoroutine(nameof(Delay));
            }
            else if (!GhostMode && PlayerPrefs.GetInt("revived") != 1)
            {
                SceneManager.LoadScene("Ghost");
                playerAudioSource.clip = hit;
                playerAudioSource.Play();
                GetComponent<SpriteRenderer>().color = new Color(255, 176, 171);
                GhostMode = true;
            }
            else
            {
                playerAudioSource.clip = hit;
                playerAudioSource.Play();
                bgAudioSource.Stop();
                if (gameOverMessage != null) gameOverMessage.SetActive(true);
                GameOver = true;
                PlayerPrefs.SetInt("revived", 0);
                StartCoroutine(nameof(Delay));
            }
        }
        else if (other.gameObject.CompareTag("soul"))
        {
            if (GhostMode)
            {
                PlayerPrefs.SetInt("revived",1);
                SceneManager.LoadScene("Game");
                playerAudioSource.clip = hit;
                playerAudioSource.Play();
                GhostMode = false;
            }
        }
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("Game Over");
    }
}
