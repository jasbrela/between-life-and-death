using Store;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        public static string HighScoreKey = "player_highscore";
        public static string ScoreKey = "player_score";
    
        private int _score;

        [SerializeField] public TextMeshProUGUI scoreText;
        [SerializeField] private AudioSource playerAudioSource;
        [SerializeField] private AudioClip collectCandy;


        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == Scenes.Game.ToString() && !PlayerStatus.Revived)
            {
                PlayerPrefs.SetInt(ScoreKey, 0);
            }
        }

        private void Update()
        {
            UpdateScore();
        }

        void UpdateScore()
        {
            _score = PlayerPrefs.GetInt(ScoreKey);
            if (scoreText != null)
            {
                scoreText.text = _score.ToString();
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Candy"))
            {
                playerAudioSource.clip = collectCandy;
                playerAudioSource.Play();
                int value = PowerUpManager.Instance.GetIsDoubleCandiesActive() ? 2 : 1;
                _score += value;
                PlayerPrefs.SetInt(ScoreKey, _score);
                Destroy(other.gameObject);
            }
        }
    }
}