using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Doces coletados nessa run")] 
    public int score;

    [Header("Todos os doces coletados")]
    public int allcandies = 100;

    [Header("Quantos pts o doce vale")] 
    public static int qtd = 1;
    
    public int highScore;

    [Header("Auxiliar pra soma dos doces")] 
    public bool gameOver;

    public TextMeshProUGUI _txtScore;

    [Header("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip colectCandy;

    private void Start()
    {
        PlayerPrefs.SetInt("allCandies", 100);
    }

    private void Update()
    {
        ScoreUpdate(); // atualiza o score
        SomaDocesTotal(); // atualiza os doces totais

        allcandies = PlayerPrefs.GetInt("allCandies");
    }

    void ScoreUpdate()
    {
        score = PlayerPrefs.GetInt("score");
        _txtScore.text = score.ToString(); // atualiza o score na ui
    }

    void SomaDocesTotal()
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
                
            PlayerPrefs.SetInt("score", 0);
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("doce")) // se colidiu com doce
        {
            playerAudioSource.clip = colectCandy;
            playerAudioSource.Play();
            score += qtd; // incrementa o score
            PlayerPrefs.SetInt("score", score);
            Destroy(col.gameObject); // destrói o doce
        }
    }
}
