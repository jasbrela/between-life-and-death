using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [Header("Doces coletados nessa run")] 
    public int score = 0;

    [Header("Doces totais")] 
    [SerializeField] private int docesTotal = 0;

    [Header("Auxiliar pra soma dos doces")] 
    public bool gameOver = false; 
    
    private TextMeshProUGUI txtScore, txtDocesTotal;

    private void Awake()
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
        txtScore.text = "Score: " + score.ToString(); // atualiza o score na ui
    }

    void SomaDocesTotal()
    {
        if (gameOver)
        {
            docesTotal += score; // soma os doces da run com o total
            score = 0; // reseta o score da run
            gameOver = false;
        }

        txtDocesTotal.text = "Doces Totais: " + docesTotal.ToString(); // atualiza o total de doces
    }
}
