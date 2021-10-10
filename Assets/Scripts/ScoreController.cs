using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Doces coletados nessa run")] 
    public int score;

    public int highScore;

    public static int qtd = 1;

    [Header("Todos os doces coletados")]
    public int allcandies = 0;
    
    public TextMeshProUGUI txtScore;
    

    [Header("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip collectCandy;


    private void Update()
    {
        UpdateScore();
        UpdateTotalCandies();
        
        allcandies = PlayerPrefs.GetInt("allCandies");
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

           allcandies += score;
           PlayerPrefs.SetInt("allCandies", allcandies);
           
           PlayerPrefs.SetInt("score", 0); // resolve bug de pontos infinitos
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("doce"))
        {
            playerAudioSource.clip = collectCandy;
            playerAudioSource.Play();
            score += qtd;
            PlayerPrefs.SetInt("score", score);
            Destroy(col.gameObject);
        }
    }
}
