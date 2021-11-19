using TMPro;
using UnityEngine;

public class PowerUpObject : MonoBehaviour
{
    [SerializeField] private PowerUp powerUpData;
    public PowerUp GetPowerUpData => powerUpData;

    private void OnEnable()
    {
        int quantity = PlayerPrefs.GetInt(powerUpData.powerUpType.ToString());
        
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = powerUpData.sprite;
        transform.Find("btn").Find("btn-buy").Find("btn-text").GetComponent<TextMeshProUGUI>().text = powerUpData.price.ToString();
        //transform.Find("quantity").GetComponent<TextMeshProUGUI>().text = quantity.ToString();
    }
}
