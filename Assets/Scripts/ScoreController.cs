using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Doces coletados nessa run")] 
    [SerializeField] private int score;

    [Header("Doces totais")] 
    [SerializeField] private int docesTotal;

    [Header("Auxiliar pra soma dos doces")] 
    [SerializeField] private bool gameOver; 
    
    private TextMeshProUGUI txtScore, txtDocesTotal;

    private void Start()
    {
        txtScore = GameObject.FindWithTag("score").GetComponent<TextMeshProUGUI>();
        txtDocesTotal = GameObject.FindWithTag("docesTotal").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        ScoreUpdate(); // atualiza o score
        SomaDocesTotal(); // atualiza os doces totais
    }

    void ScoreUpdate()
    {
        txtScore.text = score.ToString(); // atualiza o score na ui
    }

    void SomaDocesTotal()
    {
        if (gameOver)
        {
            docesTotal += score; // soma os doces da run com o total
            score = 0; // reseta o score da run
            gameOver = false;
        }

        txtDocesTotal.text = docesTotal.ToString(); // atualiza o total de doces
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("doce")) // se colidiu com doce
        {
            score++; // incrementa o score
            Destroy(col.gameObject); // destrói o doce
        }
    }
}
