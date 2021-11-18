using System.Collections;
using Store;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public static Debuff CurrentDebuff;
    public static bool GhostMode;
    public static bool GameOver;
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
                Debug.Log(PlayerPrefs.GetInt(PlayerMoney.Key) + " + " + PlayerPrefs.GetInt(PlayerScore.ScoreKey));
                PlayerPrefs.SetInt(PlayerMoney.Key, PlayerPrefs.GetInt(PlayerMoney.Key) + 
                    PlayerPrefs.GetInt(PlayerScore.ScoreKey));
                playerAudioSource.clip = hit;
                playerAudioSource.Play();
                bgAudioSource.Stop();
                GameOver = true;
                StartCoroutine(nameof(LoadGameOverScene));
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
                PlayerPrefs.SetInt("revived", 0);
                GameOver = true;
                StartCoroutine(nameof(LoadGameOverScene));
            }
        }
        else if (other.gameObject.CompareTag("soul"))
        {
            if (GhostMode)
            {
                GhostMode = false;

                PlayerPrefs.SetInt("revived",1);
                SceneManager.LoadScene("Game");
                playerAudioSource.clip = hit;
                playerAudioSource.Play();
            }
        }
    }
    
    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("Game Over");
    }
}
