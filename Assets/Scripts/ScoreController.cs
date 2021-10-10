using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Doces coletados nessa run")] 
    public int score;

    [Header("Auxiliar pra soma dos doces")] 
    public bool gameOver;

    public TextMeshProUGUI _txtScore;

    [Header("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip colectCandy;

    private void Start()
    {
    }

    private void Update()
    {
        ScoreUpdate(); // atualiza o score
        SomaDocesTotal(); // atualiza os doces totais
    }

    void ScoreUpdate()
    {
        score = PlayerPrefs.GetInt("score");
        _txtScore.text = score.ToString(); // atualiza o score na ui
    }

    void SomaDocesTotal()
    {
        if (gameOver)
        {
            score = 0; // reseta o score da run
            PlayerPrefs.SetInt("score", 0);
            gameOver = false;
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("doce")) // se colidiu com doce
        {
            playerAudioSource.clip = colectCandy;
            playerAudioSource.Play();
            score++; // incrementa o score
            PlayerPrefs.SetInt("score", score);
            Destroy(col.gameObject); // destrói o doce
        }
    }
}
