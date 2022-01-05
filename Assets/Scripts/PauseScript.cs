using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    private bool pause = false;
    [SerializeField] private GameObject pauseCanvas;

    [SerializeField] private Toggle sfx;
    [SerializeField] private Toggle music;

    [SerializeField] private AudioMixer audioMixer;
    // Start is called before the first frame update
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
    public void Pause()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            pauseCanvas.SetActive(true);
            pause = true;
        }
    }

    public void Resume()
    {
        if (pause)
        {
            Time.timeScale = 1;
            pauseCanvas.SetActive(false);
            pause = false;
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
