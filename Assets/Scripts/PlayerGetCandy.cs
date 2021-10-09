using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetCandy : MonoBehaviour
{
    private ScoreController scriptScoreController;
    
    void Awake()
    {
        scriptScoreController = GameObject.FindWithTag("scoreManager").GetComponent<ScoreController>();
    }
    
    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag == "doce") // se colidiu com doce
        {
            scriptScoreController.score++; // incrementa o score
            Destroy(col.gameObject); // destrói o doce
        }
    }
}
