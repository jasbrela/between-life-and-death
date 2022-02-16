using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [Header("Any Scene")]
    [SerializeField] private AudioSource audioSource;

    [Header("Game Over Scene ONLY")]
    [SerializeField] private TMP_Text gameOverScore;

    
    [Header("Main Menu Scene ONLY")]
    [SerializeField] private GameObject panelCredits;
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private Toggle music;
    [SerializeField] private Toggle sfx;
    [SerializeField] private AudioMixer audioMixer;
    
    private void Start()
    {
        if (music != null)
        {
            if (PlayerPrefs.GetInt("musicToggle") == -1)
            {
                music.isOn = false;
                MusicToggleClick(music);
            }
            else
            {
                music.isOn = true;
                MusicToggleClick(music);
            }

            if (PlayerPrefs.GetInt("sfxToggle") == -1)
            {
                sfx.isOn = false;
                SfxToggleClick(sfx);
            }
            else
            {
                sfx.isOn = true;
                SfxToggleClick(sfx);
            }
        }

        if (gameOverScore != null)
        {
            int score = PlayerPrefs.GetInt(PlayerScore.ScoreKey);
            string plural = score == 1 ? "" : "s";
            gameOverScore.text += score + " point" + plural;
        }
    }
    
    public void StartGame()
    {
        Reset();
        LoadSelectedScene(Scenes.Game.ToString());
        
        PlayerPrefs.SetInt(PlayerScore.ScoreKey, 0);
        audioSource.Play();
    }

    public void Store()
    {
        SceneManager.LoadScene(Scenes.Store.ToString());
        audioSource.Play();
    }

    private void Reset()
    {
        PlayerStatus.currentDebuff = DebuffType.None;
        PlayerStatus.isGhostMode = false;
        PlayerStatus.isGameOver = false;
    }

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
        if (PlayerStatus.isPaused) PlayerStatus.isPaused = false;
        
        if (sceneName != Scenes.Ghost.ToString())
        {
            PlayerStatus.isGhostMode = false;
        }
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt(PlayerScore.ScoreKey, 0);
    }

    public void MusicToggleClick(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            audioMixer.SetFloat("Music", -88);
            PlayerPrefs.SetInt("musicToggle", -1);
        }
        else
        {
            audioMixer.SetFloat("Music", 0);
            PlayerPrefs.SetInt("musicToggle", 1);
        }
    }

    public void SfxToggleClick(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            audioMixer.SetFloat("SFX", -88);
            PlayerPrefs.SetInt("sfxToggle", -1);
        }
        else
        {
            audioMixer.SetFloat("SFX", 0);
            PlayerPrefs.SetInt("sfxToggle", 1);
        }
    }
}