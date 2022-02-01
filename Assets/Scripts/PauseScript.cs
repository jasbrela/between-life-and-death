using Player;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private Image pauseBtn;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite resumeSprite;
    
    [SerializeField] private GameObject pauseCanvas;

    [SerializeField] private Toggle sfx;
    [SerializeField] private Toggle music;

    [SerializeField] private AudioMixer audioMixer;

    private float _timeScale;
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
    }

    public void TogglePause()
    {
        if (!PlayerStatus.isPaused)
        {
            pauseBtn.sprite = resumeSprite;
            _timeScale = Time.timeScale;
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
            PlayerStatus.isPaused = true;
        }
        else
        {
            pauseBtn.sprite = pauseSprite;
            Time.timeScale = _timeScale;
            pauseCanvas.SetActive(false);
            PlayerStatus.isPaused = false;
        }
    }

    public void Menu()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        SceneManager.LoadScene("Menu");
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
