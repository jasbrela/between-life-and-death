using System;
using System.Collections;
using Enums;
using Store;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private AllPowerUpData allPowerUps;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] [Range(1f, 5f)] private float maxVelocity;
        
        private const float DebuffMultiplier = 1.2f;
        private float _timer = 1;
    
        public static DebuffType currentDebuff;
        public static bool isGhostMode;
        public static bool isGameOver;
        public static bool isPaused;
        public static bool hasRevived;
    
        [Header("Audio")]
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip hit;
        [SerializeField] private AudioSource bgAudioSource;
        [SerializeField] private AudioClip powerUp;
        
        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == Scenes.Ghost.ToString())
            {
                isGhostMode = true;
            }
                
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
                while (isPaused || isGameOver || !Application.isFocused) yield return null;
                
                yield return new WaitForSecondsRealtime(interval);
                
                float multiplier = currentDebuff == DebuffType.IncreasedSpeed ? DebuffMultiplier : 1f;
                _timer += interval * Time.unscaledDeltaTime * multiplier * 0.1f;
            }
        }

        private void Update()
        {
            if (!isPaused && Application.isFocused)
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
                    hasRevived = false;
                    isGameOver = true;
                    UpdateHighScore();
                    AddMoneyBasedOnScore();
                    StartCoroutine(LoadSceneAfterInterval(Scenes.GameOver));
                }
                else if (!isGhostMode && !hasRevived) // (1) DIED in HUMAN MODE. -> GHOST MODE
                {
                    Time.timeScale = (Time.timeScale - 1)/3 + 1;
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    isGhostMode = true;
                    StartCoroutine(LoadSceneAfterInterval(Scenes.Ghost));
                }
                else // DIED in HUMAN MODE, after reviving -> GAME OVER
                {
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    hasRevived = false;
                    isGameOver = true;
                    UpdateHighScore();
                    AddMoneyBasedOnScore();
                    StartCoroutine(LoadSceneAfterInterval(Scenes.GameOver));
                }
            }
            else if (other.CompareTag("Soul")) // COLLECT SOUL -> HUMAN MODE
            {
                if (isGhostMode)
                {
                    playerAudioSource.clip = powerUp;
                    playerAudioSource.Play();
                    
                    isGhostMode = false;
                    hasRevived = true;
                    StartCoroutine(LoadSceneAfterInterval(Scenes.Game));
                }
            }
            else
            {
                CheckForPowerUps(other.gameObject);
            }
        }

        private void CheckForPowerUps(GameObject toBeChecked)
        {
            string prefix = "PowerUp/";
            PowerUpType? find = null;

            foreach (PowerUpType powerUp in Enum.GetValues(typeof(PowerUpType)))
            {
                if (powerUp == PowerUpType.None) continue;
                if (!toBeChecked.CompareTag(prefix + powerUp)) continue;
                
                Destroy(toBeChecked);
                find = powerUp;
                break;
            }

            if (find == null)
            {
                return;
            }
            
            switch (find)
            {
                case PowerUpType.CandyMagnet:
                    PowerUpManager.Instance.UsePowerUp(allPowerUps.candyMagnetData);
                    playerAudioSource.clip = powerUp;
                    playerAudioSource.Play();
                    break;
                case PowerUpType.DoubleCandies:
                    PowerUpManager.Instance.UsePowerUp(allPowerUps.doubleCandiesData);
                    playerAudioSource.clip = powerUp;
                    playerAudioSource.Play();
                    break;
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
    
        private IEnumerator LoadSceneAfterInterval(Scenes scene)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            SceneManager.LoadScene(scene.ToString());
        }

        public static void ResetValues()
        {
            isPaused = false;
            isGhostMode = false;
            isGameOver = false;
            hasRevived = false;
        }
    }
}
