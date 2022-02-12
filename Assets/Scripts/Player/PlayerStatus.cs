using System.Collections;
using Enums;
using Store;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve;
        [SerializeField] [Range(1f, 5f)] private float maxVelocity;
        
        private const float DebuffMultiplier = 1.2f;
        private float _timer = 1;
        public static bool Revived { get; private set; }
    
        public static DebuffType currentDebuff;
        public static bool isGhostMode;
        public static bool isGameOver;
        public static bool isPaused;
    
        [Header("Audio")]
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip hit;
        [SerializeField] private AudioSource bgAudioSource;
        
        private void Awake()
        {
            if (!isGhostMode)
            {
                Time.timeScale = 1;
                currentDebuff = DebuffType.None;
                isGameOver = false;
            }
        }

        private void Start()
        {
            StartCoroutine(Tick(0.1f));
        }

        private IEnumerator Tick(float interval)
        {
            while (true)
            {
                if (isPaused || isGameOver) yield return null;
                yield return new WaitForSecondsRealtime(interval);
                
                float multiplier = currentDebuff == DebuffType.HigherVelocity ? DebuffMultiplier : 1f;
                _timer += interval * Time.unscaledDeltaTime * multiplier * 0.1f;
            }
        }

        private void Update()
        {
            if (!isPaused)
            {
                if (Time.timeScale < maxVelocity)
                {
                    Time.timeScale = curve.Evaluate(_timer);
                }
            }

            if (isGameOver)
            {
                isPaused = false;
                Time.timeScale = 0;
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Obstacle"))
            {
                if (isGhostMode) // (2) DIED in GHOST MODE -> GAME OVER
                {
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    Revived = false;
                    isGameOver = true;
                    UpdateHighScore();
                    AddMoneyBasedOnScore();
                    StartCoroutine(nameof(LoadGameOverScene));
                }
                else if (!isGhostMode && !Revived) // (1) DIED in HUMAN MODE. -> GHOST MODE
                {
                    Time.timeScale = (Time.timeScale - 1)/3 + 1;
                    SceneManager.LoadScene(Scenes.Ghost.ToString());
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    isGhostMode = true;
                }
                else // DIED in HUMAN MODE, after reviving -> GAME OVER
                {
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    Revived = false;
                    isGameOver = true;
                    UpdateHighScore();
                    AddMoneyBasedOnScore();
                    StartCoroutine(nameof(LoadGameOverScene));
                }
            }
            else if (other.CompareTag("Soul")) // COLLECT SOUL -> HUMAN MODE
            {
                if (isGhostMode)
                {
                    isGhostMode = false;
                    Revived = true;
                    SceneManager.LoadScene(Scenes.Game.ToString());
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                }
            }
            else if (other.CompareTag("PowerUp/" + PowerUpType.CandyMagnet))
            {
                PowerUpManager.Instance.UsePowerUp(PowerUpType.CandyMagnet);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("PowerUp/" + PowerUpType.DoubleCandies))
            {
                PowerUpManager.Instance.UsePowerUp(PowerUpType.DoubleCandies);
                Destroy(other.gameObject);
            }
        }

        private void UpdateHighScore()
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
