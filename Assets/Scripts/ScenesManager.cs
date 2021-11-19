using Player;
using Store;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{

    public static string MenuScene = "Menu";
    public static string StoreScene = "Store";
    public static string HumanGameScene = "Game";
    public static string GhostGameScene = "Ghost";
    public static string GameOverScene = "Game Over";

    [SerializeField] private string startGameSceneName;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Toggle music;
    [SerializeField] private Toggle sfx;

    [SerializeField][Tooltip("Only for GAME OVER's scene")] private TMP_Text gameOverScore;

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
            gameOverScore.text += PlayerPrefs.GetInt(PlayerScore.ScoreKey).ToString();
        }
    }

    [SerializeField] private AudioMixer audioMixer;

    public void StartGame()
    {
        Reset();
        SceneManager.LoadScene(startGameSceneName);
        if (startGameSceneName == HumanGameScene)
        {
            PowerUpManager.Instance.UseAllPowerUps();
        }
        PlayerPrefs.SetInt(PlayerScore.ScoreKey, 0);
        audioSource.Play();
    }

    public void Store()
    {
        SceneManager.LoadScene("Store");
        audioSource.Play();
    }

    private void Reset()
    {
        PlayerStatus.currentDebuffType = DebuffType.None;
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
        if(sceneName != GhostGameScene)
        {
            PlayerStatus.GhostMode = false;
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