using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public static Debuff CurrentDebuff;
    public static bool GhostMode;
    public static bool GameOver;
    [SerializeField] private GameObject gameOverMessage;
    private float _currentTimeScale = 1;
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
                bgaudioSource.Stop();
                gameOverMessage.SetActive(true);
                GameOver = true;
                StartCoroutine("Delay");
                Time.timeScale = 0;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Ghost");
                playeraudioSource.clip = hit;
                playeraudioSource.Play();
                GetComponent<SpriteRenderer>().color = Color.blue;
                GhostMode = true;
                // ativa modo ghost
            }
        }
    }
    
    IEnumerator Delay()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }
}
