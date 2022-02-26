using System;
using Enums;
using Player;
using Store;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerUpBar : MonoBehaviour
{
    [SerializeField] private GameObject bar;
    [SerializeField] private Image filling;
    [SerializeField] private Image icon;
    [SerializeField] private AllPowerUpData allPowerUpData;

    private float _duration;
    private bool _canDecreaseBar;

    private void Start()
    {
        PowerUpManager.Instance.SubscribeToPowerUpTimer(SetUpBar, OnTimerStarts, HidePowerUpBar);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void SetUpBar(PowerUpType type)
    {
        PowerUp data;
        
        switch (type)
        {
            case PowerUpType.CandyMagnet:
                data = allPowerUpData.candyMagnetData;
                break;
            case PowerUpType.DoubleCandies:
                data = allPowerUpData.doubleCandiesData;

                break;
            default:
                HidePowerUpBar();
                return;
        }
        
        _duration = data.durations[PlayerPrefs.GetInt(type + "_level")].value;
        icon.sprite = data.sprite;
        
        filling.fillAmount = 1;
        ShowPowerUpBar();
    }

    private void Update()
    {
        if (!(_duration > 0) || !_canDecreaseBar) return;
        
        var percentage = PowerUpManager.Instance.GetCount() / _duration;
        if (percentage > 1) percentage = 1;
        filling.fillAmount = percentage;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PowerUpManager.Instance.UnsubscribeToPowerUpTimer(SetUpBar, OnTimerStarts, HidePowerUpBar);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnTimerStarts()
    {
        _canDecreaseBar = true;
    }

    private void ShowPowerUpBar()
    {
        bar.SetActive(true);
    }

    private void HidePowerUpBar()
    {
        bar.SetActive(false);
        _canDecreaseBar = false;
    }
}
