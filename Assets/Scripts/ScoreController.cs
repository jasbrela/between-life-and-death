using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Doces coletados nessa run")] 
    public int score;

    [Header("Doces totais")] 
    [SerializeField] private int docesTotal;

    [Header("Auxiliar pra soma dos doces")] 
    public bool gameOver; 
    
    private TextMeshProUGUI _txtScore, _txtDocesTotal;

    [Header("Audio")]
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioClip colectCandy;

    private void Start()
    {
        _txtScore = GameObject.FindWithTag("score").GetComponent<TextMeshProUGUI>();
        _txtDocesTotal = GameObject.FindWithTag("docesTotal").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        ScoreUpdate(); // atualiza o score
        SomaDocesTotal(); // atualiza os doces totais
    }

    void ScoreUpdate()
    {
        _txtScore.text = score.ToString(); // atualiza o score na ui
    }

    void SomaDocesTotal()
    {
        if (gameOver)
        {
            docesTotal += score; // soma os doces da run com o total
            score = 0; // reseta o score da run
            gameOver = false;
        }

        _txtDocesTotal.text = docesTotal.ToString(); // atualiza o total de doces
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("doce")) // se colidiu com doce
        {
            playerAudioSource.clip = colectCandy;
            playerAudioSource.Play();
            score++; // incrementa o score
            Destroy(col.gameObject); // destrói o doce
        }
    }
}
