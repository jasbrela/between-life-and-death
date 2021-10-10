using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SceneController : MonoBehaviour
{
    [Header("Nome da cena de gameplay")] [SerializeField]
    private string startGameSceneName;

    [SerializeField] private AudioSource audioSource;

    [Header("Score na scene Game Over")] [SerializeField]
    private TMP_Text gameOverText;

    private void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.text += PlayerPrefs.GetInt("score").ToString();
        }
    }

    [SerializeField] private AudioMixer audioMixer;

    public void StartGame()
    {
        Reset();
        SceneManager.LoadScene(startGameSceneName);
        PlayerPrefs.SetInt("score", 0);
        audioSource.Play();
    }

    public void Store()
    {
        SceneManager.LoadScene("Store");
        audioSource.Play();
    }

    private void Reset()
    {
        PlayerStatus.CurrentDebuff = Debuff.None;
        PlayerStatus.GhostMode = false;
        PlayerStatus.GameOver = false;
    }

    [Header("Painel dos creditos")] [SerializeField]
    private GameObject panelCredits;

    [Header("Painel do Menu")] [SerializeField]
    private GameObject panelMenu;

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

    public void musicToggleClick(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            audioMixer.SetFloat("Music", -88);
        }
        else
        {
            audioMixer.SetFloat("Music", 0);
        }
    }

    public void sfxToggleClick(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            audioMixer.SetFloat("SFX", -88);
        }
        else
        {
            audioMixer.SetFloat("SFX", 0);
        }
    }
}