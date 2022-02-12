using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PowerUpObject : MonoBehaviour
{
    private const int MaxLevel = 5;
    [SerializeField] private PowerUp powerUpData;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI buyPrice;
    [SerializeField] private Image fill;
    
    public PowerUp GetPowerUpData => powerUpData;

    private void OnEnable()
    {
        UpdateUI();
        
        icon.sprite = powerUpData.sprite;
        buyPrice.text = powerUpData.price.ToString();
    }

    private void UpdateUI()
    {
        var level = PlayerPrefs.GetInt(powerUpData.powerUpType + "_level");
        fill.fillAmount = level > 0
            ? PlayerPrefs.GetInt(powerUpData.powerUpType + "_level") / (float) MaxLevel
            : 0f;
    }

    public void OnClickBuy()
    {
        UpdateUI();
    }
}
