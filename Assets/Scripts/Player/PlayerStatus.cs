using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        private const float Velocity = 0.000005f;
        [SerializeField] [Range(1f, 5f)] private float maxVelocity;
        private const string SoulTag = "Soul";
        public static bool Revived { get; private set; }
    
        public static DebuffType currentDebuffType;
        public static bool GhostMode;
        public static bool GameOver;
        public static bool isPaused;
    
        [Header("Audio")]
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip hit;
        [SerializeField] private AudioSource bgAudioSource;
        
        private void Awake()
        {
            if (!GhostMode)
            {
                Time.timeScale = 1;
                currentDebuffType = DebuffType.None;
                GameOver = false;
            }
        }

        private void Update()
        {
            if (!isPaused)
            {
                if (currentDebuffType != DebuffType.HigherVelocity)
                {
                    if (Time.timeScale < maxVelocity)
                    {
                        Time.timeScale += Velocity;
                    }
                }
                else
                {
                    if (Time.timeScale < maxVelocity)
                    {
                        Time.timeScale += Velocity * 1.2f;
                    }
                }
            }

            if (GameOver)
            {
                isPaused = false;
                Time.timeScale = 0;
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                if (GhostMode) // (2) DIED in GHOST MODE -> GAME OVER
                {
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    GameOver = true;
                    UpdateHighScore();
                    AddMoneyBasedOnScore();
                    StartCoroutine(nameof(LoadGameOverScene));
                }
                else if (!GhostMode && !Revived) // (1) DIED in HUMAN MODE. -> GHOST MODE
                {
                    Time.timeScale = (Time.timeScale - 1)/3 + 1;
                    SceneManager.LoadScene(SceneController.GhostGameScene);
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    GhostMode = true;
                }
                else // DIED in HUMAN MODE, after reviving -> GAME OVER
                {
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    Revived = false;
                    GameOver = true;
                    UpdateHighScore();
                    AddMoneyBasedOnScore();
                    StartCoroutine(nameof(LoadGameOverScene));
                }
            }
            else if (other.gameObject.CompareTag(SoulTag)) // COLLECT SOUL -> HUMAN MODE
            {
                if (GhostMode)
                {
                    GhostMode = false;
                    Revived = true;
                    SceneManager.LoadScene(SceneController.HumanGameScene);
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                }
            }
        }

        public void UpdateHighScore()
        {
            if (PlayerPrefs.GetInt(PlayerScore.ScoreKey) > PlayerPrefs.GetInt(PlayerScore.HighScoreKey))
            {
                PlayerPrefs.SetInt(PlayerScore.HighScoreKey, PlayerPrefs.GetInt(PlayerScore.ScoreKey));
            }
        }
        
        private void AddMoneyBasedOnScore()
        {
            PlayerPrefs.SetInt(PlayerMoney.Key, PlayerPrefs.GetInt(PlayerMoney.Key) + 
                                                PlayerPrefs.GetInt(PlayerScore.ScoreKey));
        }
    
        IEnumerator LoadGameOverScene()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            SceneManager.LoadScene("Game Over");
        }
    }
}
