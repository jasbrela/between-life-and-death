using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        private const string SoulTag = "Soul";
        private static bool revived;
    
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
            Debug.Log("Já reviveu? " + revived);
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
                if (GhostMode) // (2) Fantasma
                {
                    PlayerPrefs.SetInt(PlayerMoney.Key, PlayerPrefs.GetInt(PlayerMoney.Key) + 
                                                        PlayerPrefs.GetInt(PlayerScore.ScoreKey));
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    GameOver = true;
                    StartCoroutine(nameof(LoadGameOverScene));
                }
                else if (!GhostMode && !revived) // (1) Humano, mas ainda não reviveu
                {
                    SceneManager.LoadScene(ScenesManager.GhostGameScene);
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    GetComponent<SpriteRenderer>().color = new Color(255, 176, 171);
                    GhostMode = true;
                }
                else // (3) Humano que reviveu ou fantasma.
                {
                    playerAudioSource.clip = hit;
                    playerAudioSource.Play();
                    bgAudioSource.Stop();
                    revived = false;
                    GameOver = true;
                    StartCoroutine(nameof(LoadGameOverScene));
                }
            }
            else if (other.gameObject.CompareTag(SoulTag))
            {
                if (GhostMode)
                {
                    GhostMode = false;
                    revived = true;
                    SceneManager.LoadScene(ScenesManager.HumanGameScene);
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
}
