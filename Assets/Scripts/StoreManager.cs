using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    [Header("Item q player comprou")]
    public int item = 0;
    
    public int idSkin = 0;
    
    public int allCandies = 0;

    public RuntimeAnimatorController[] skinsAnim;

    private GameObject player;

    private TextMeshProUGUI txtDocesTotal;

    private void Start()
    {
        txtDocesTotal = GameObject.FindWithTag("docesTotal").GetComponent<TextMeshProUGUI>();
        
        if (SceneManager.GetActiveScene().name == "Game")
        {
            player = GameObject.FindWithTag("Player");
            CheckSkin();
        }
        
        item = PlayerPrefs.GetInt("Item");
        
        GameObject obj = GameObject.FindWithTag("storeManager");
        if (obj.gameObject != this.gameObject)
        {
            Destroy(obj.gameObject);
        }
    }

    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
        allCandies = PlayerPrefs.GetInt("allCandies");
        
        txtDocesTotal.text = allCandies.ToString();

        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Ghost")
        {
            StartCoroutine("Delay", 10f);
        }
    }

    private void CheckSkin()
    {
        player.GetComponent<Animator>().runtimeAnimatorController = skinsAnim[idSkin];
    }

    public void MultiplieCandies()
    {
        if (allCandies >= 10 && item == 0) // se tem doces o suficiente e se nao comprou nenhum outro
        {
            allCandies -= 10;
            PlayerPrefs.SetInt("allCandies", allCandies);
            item = 1;
            PlayerPrefs.SetInt("Item", item);
        }
    }
    
    public void MagnetCandies()
    {
        if (allCandies >= 10 && item == 0) // se tem doces o suficiente e se nao comprou nenhum outro
        {
            allCandies -= 10;
            PlayerPrefs.SetInt("allCandies", allCandies);
            item = 2;
            PlayerPrefs.SetInt("Item", item);
        }
    }

    public void NoObstacles()
    {
        if (allCandies >= 10 && item == 0) // se tem doces o suficiente e se nao comprou nenhum outro
        {
            allCandies -= 10;
            PlayerPrefs.SetInt("allCandies", allCandies);
            item = 3;
            PlayerPrefs.SetInt("Item", item);
        }
    }

    public void Skin1()
    {
        idSkin = 0;
        PlayerPrefs.SetInt("idSkin", idSkin);
    }

    public void Skin2()
    {
        idSkin = 1;
        PlayerPrefs.SetInt("idSkin", idSkin);
    }
    
    public void Skin3()
    {
        idSkin = 2;
        PlayerPrefs.SetInt("idSkin", idSkin);
    }
    
    IEnumerator Delay()
    {
        switch (item)
        {
            case 0: // nao usou nada
                ScoreController.qtd = 1;
                // candieMultiplies = false;
                break;
            case 1: // multiplicador de doce
                ScoreController.qtd = 2;
                break;
            case 2: // doces na direção do player
                GameObject.FindWithTag("doce").transform.Translate(
                    GameObject.FindWithTag("Player").transform.position * 2 * Time.deltaTime);
                break;
            case 3:
                break;
        }
        yield return new WaitForSeconds(10f); // o tempo do powerup
        item = 0; // para de usar
        PlayerPrefs.SetInt("Item", item);
    }
}
