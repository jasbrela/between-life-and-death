using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        private const string SoulTag = "Soul";
        public static bool Revived { get; private set; }
    
        public static DebuffType currentDebuffType;
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
                currentDebuffType = DebuffType.None;
                GameOver = false;
            }
        }

        private void Update()
        {
            Debug.Log("Já reviveu? " + Revived);
            if (currentDebuffType != DebuffType.HigherVelocity)
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
                if (GhostMode) // (2) DIED in GHOST MODE -> GAME OVER
                {
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    GameOver = true;
                    AddMoneyBasedOnScore();
                    StartCoroutine(nameof(LoadGameOverScene));
                }
                else if (!GhostMode && !Revived) // (1) DIED in HUMAN MODE. -> GHOST MODE
                {
                    SceneManager.LoadScene(ScenesManager.GhostGameScene);
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    GetComponent<SpriteRenderer>().color = new Color(255, 176, 171);
                    GhostMode = true;
                }
                else // DIED in HUMAN MODE, after reviving -> GAME OVER
                {
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    Revived = false;
                    GameOver = true;
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
                    SceneManager.LoadScene(ScenesManager.HumanGameScene);
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                }
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
