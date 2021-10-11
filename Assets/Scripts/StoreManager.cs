using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    private static StoreManager _instance;
    [Header("Item q player comprou")]
    public int item = 0;
    
    public int idSkin = 0;
    
    public int allCandies = 0;

    public RuntimeAnimatorController[] skinsAnim;

    private GameObject player, useGo;

    private TextMeshProUGUI txtDocesTotal;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void Awake()
    {
      
        if (_instance == null) {
            DontDestroyOnLoad(this);
            _instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        switch (scene.name)
        {
            case "Store":
                if (GameObject.Find("AllCandies"))
                {
                    txtDocesTotal = GameObject.FindWithTag("docesTotais").GetComponent<TextMeshProUGUI>();
                }
                break;
            case "Game":
                if (GameObject.FindWithTag("Player"))
                {
                    player = GameObject.FindWithTag("Player");
                }
                CheckSkin();
                break;
        }
        
        item = PlayerPrefs.GetInt("Item");
    }

    void Update()
    {
        allCandies = PlayerPrefs.GetInt("allCandies");

        switch (SceneManager.GetActiveScene().name)
        {
            case "Game":
            case "Ghost":
                StartCoroutine("Delay", 10f);
                /*
                if (PlayerPrefs.GetInt("Item") != 0)
                {
                    GameObject.Find("Use").SetActive(true);
                }
                else
                {
                    GameObject.Find("Use").SetActive(false);
                }
                */
                break;
            case "Store":
                txtDocesTotal.text = allCandies.ToString();
                break;
        }
    }

    private void CheckSkin()
    {
        player.GetComponent<Animator>().runtimeAnimatorController = skinsAnim[idSkin];
    }

    public void MultiplieCandies()
    {
        if (allCandies >= 10) // se tem doces o suficiente e se nao comprou nenhum outro
        {
            allCandies -= 10;
            PlayerPrefs.SetInt("allCandies", allCandies);
            item = 1;
            PlayerPrefs.SetInt("Item", item);
        }
    }
    /*
    public void UsePowerUp()
    {
        StartCoroutine("Delay", 10f);
    }
    */
    
    public void MagnetCandies()
    {
        if (allCandies >= 10) // se tem doces o suficiente e se nao comprou nenhum outro
        {
            allCandies -= 10;
            PlayerPrefs.SetInt("allCandies", allCandies);
            item = 2;
            PlayerPrefs.SetInt("Item", item);
        }
    }

    public void NoObstacles()
    {
        if (allCandies >= 10) // se tem doces o suficiente e se nao comprou nenhum outro
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
                item = 10;
                PlayerPrefs.SetInt("Item", item);
                break;
        }
        yield return new WaitForSeconds(10f); // o tempo do powerup
        item = 0; // para de usar
        PlayerPrefs.SetInt("Item", item);
    }
}
