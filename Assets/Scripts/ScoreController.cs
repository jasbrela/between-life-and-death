using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    [Header("Doces coletados nessa run")] 
    public int score;

    public int highScore;

    public static int qtd = 1;

    [Header("Todos os doces coletados")]
    public int allcandies;
    
    public TextMeshProUGUI txtScore;
    
    [Header("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip collectCandy;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private bool oneTime = false;

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Game Over" && !oneTime)
        {
            oneTime = true;
            allcandies += score;
            PlayerPrefs.SetInt("allCandies", allcandies);
        }
    }
    
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
