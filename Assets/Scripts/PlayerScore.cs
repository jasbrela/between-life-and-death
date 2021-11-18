using Store;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

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
        if (scene.name == "Game")
        {
            PlayerPrefs.SetInt(ScoreKey, 0);
        } else if (scene.name == "Ghost")
        {
            // while ghost don't increase score
            UpdateHighScore();
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

    void UpdateHighScore()
    {
        if (PlayerStatus.GameOver)
        {
            if (_score > PlayerPrefs.GetInt(HighScoreKey))
            {
                PlayerPrefs.SetInt(HighScoreKey, _score);
            }
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