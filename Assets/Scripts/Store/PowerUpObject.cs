using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpObject : MonoBehaviour
{
    private const int MaxLevel = 5;
    [SerializeField] private PowerUp powerUpData;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI buyPrice;
    [SerializeField] private Image fill;
    [SerializeField] private GameObject buyButton;
    
    public PowerUp GetPowerUpData => powerUpData;

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var level = PlayerPrefs.GetInt(powerUpData.type + "_level");
        
        icon.sprite = powerUpData.sprite;
        
        fill.fillAmount = level > 0
            ? level / (float) MaxLevel
            : 0f;

        if (level < MaxLevel)
        {
            buyPrice.text = powerUpData.prices[level].value.ToString();
        }
        else
        {
            buyButton.SetActive(false);
        }
    }

    public void OnClickBuy()
    {
        UpdateUI();
    }
}
