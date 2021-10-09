using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeDeathController : MonoBehaviour
{
    [Header("Virou fantasma?")]
    public bool ghostPhase = false;

    private GameObject scriptScoreController; // Auxiliar pra variavel de outro script
    
    [Header("O jogo virou txt xD")] 
    public  GameObject txtPhase;

    // mudar a animação tbm
    [Header("Sprites do player (0 - humano, 1 - fantasma)")]
    public Sprite[] playerSprite;
    
    void Awake()
    {
        scriptScoreController = GameObject.FindWithTag("scoreManager");
    }

    void Update()
    {
        PhaseManager(); // mudancas pras fases humano/fantasma
    }

    void PhaseManager()
    {
        if (ghostPhase) // se é fantasma 
        {
            // ### aqui vao as mudancas pro mundo fantasma
            
            // doces vao sumir
            if (GameObject.FindWithTag("doce"))
            {
                Destroy(GameObject.FindWithTag("doce"));
            }
            // tbm poderia usar ghostPhase pra desabilitar doces no spawner 
            
            // muda cenario
            
            // muda player
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite[1];
            // colocar a animação tbm
        }
        else // é humano
        {
            // ### aqui vao as mudancas pro mundo humano
            
            // muda cenario
            
            // muda player
            this.gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite[0];
            // colocar a animação tbm
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Obstacle") // se colidiu com obstaculo
        {
            if (!ghostPhase) // se for humano
            {
                ghostPhase = true; // vira fantasma
                txtPhase.SetActive(true);
                StartCoroutine("DelayPhase", 2f);
            }
            else // se for fantasma
            {
                scriptScoreController.GetComponent<ScoreController>().gameOver = true; // game over
                DontDestroyOnLoad(scriptScoreController);
                SceneManager.LoadScene("Game Over");
            }
        }
    }

    IEnumerator DelayPhase()
    {
        yield return new WaitForSeconds(2f);
        txtPhase.SetActive(false);
    }
}
