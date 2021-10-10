using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Nome da cena de gameplay")]
    [SerializeField] private string startGameSceneName;
    [SerializeField] private AudioSource audioSource;

    [Header("Score na scene Game Over")]
    [SerializeField] private TMP_Text gameOverText;

    private void Start()
    {
        gameOverText.text += PlayerPrefs.GetInt("score").ToString();
    }

    public void StartGame()
    {
        Reset();
        SceneManager.LoadScene(startGameSceneName);
        PlayerPrefs.SetInt("score", 0);
        audioSource.Play();

    }

    private void Reset()
    {
        PlayerStatus.CurrentDebuff = Debuff.None;
        PlayerStatus.GhostMode = false;
        PlayerStatus.GameOver = false;
    }

    [Header("Painel dos creditos")]
    [SerializeField] private GameObject panelCredits;
    
    [Header("Painel do Menu")]
    [SerializeField] private GameObject panelMenu;
    public void ShowCreditsPanel()
    {
        audioSource.Play();
        panelMenu.SetActive(false);
        panelCredits.SetActive(true);
    }
    
    public void HideCreditsPanel()
    {
        audioSource.Play();
        panelMenu.SetActive(true);
        panelCredits.SetActive(false);
    }

    public void LoadSelectedScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
