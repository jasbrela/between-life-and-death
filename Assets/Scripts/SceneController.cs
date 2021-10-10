using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SceneController : MonoBehaviour
{
    [Header("Nome da cena de gameplay")]
    [SerializeField] private string startGameSceneName;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource;

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

    private bool _credits;
    [Header("Painel dos creditos")]
    public GameObject panelCredits;
    public void CreditsPanel()
    {
        audioSource.Play();
        _credits =! _credits;
        if (_credits)
        {
            panelCredits.SetActive(true);
        }
        else
        {
            panelCredits.SetActive(false);
        }
    }

    public void musicToggleClick(Toggle music)
    {
        if (!music.isOn)
        {
            audioMixer.SetFloat("Music", -88);
        }
        else
        {
            audioMixer.SetFloat("Music", 0);
        }
    }

    public void sfxToggleClick(Toggle sfx)
    {
        if (!sfx.isOn)
        {
            audioMixer.SetFloat("SFX", -88);
        }
        else
        {
            audioMixer.SetFloat("SFX", 0);
        }
    }
}
