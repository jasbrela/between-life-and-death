using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Doces coletados nessa run")] 
    public int score;

    public int highScore;

    public TextMeshProUGUI txtScore;

    [Header("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip collectCandy;


    private void Update()
    {
        UpdateScore();
        UpdateTotalCandies();
    }

    void UpdateScore()
    {
        score = PlayerPrefs.GetInt("score");
        txtScore.text = score.ToString();
    }

    void UpdateTotalCandies()
    {
        if (PlayerStatus.GameOver)
        {
           if(score > highScore)
           {
               highScore = score;
               PlayerPrefs.SetInt("highscore", highScore);
           }
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("doce"))
        {
            playerAudioSource.clip = collectCandy;
            playerAudioSource.Play();
            score++;
            PlayerPrefs.SetInt("score", score);
            Destroy(col.gameObject);
        }
    }
}
