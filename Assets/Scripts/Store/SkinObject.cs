using Enums;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class SkinObject : MonoBehaviour
    {
        [SerializeField] private Skin skinData;
        [SerializeField] private TextMeshProUGUI price;
        [SerializeField] private GameObject buyButton;
        [SerializeField] private GameObject equipButton;
        [SerializeField] private Image icon;
        
        public Skin SkinData => skinData;

        private void OnEnable()
        {
            if (skinData.Price == -1 || skinData.sprite == null)
            {
                gameObject.SetActive(false);
                return;
            }
            
            bool isUnlocked = PlayerPrefs.GetInt(
                $"skin_{skinData.character.ToString()}_{skinData.type}") == 1;

            if (isUnlocked || skinData.type == SkinType.Default)
            {
                buyButton.SetActive(false);
                equipButton.SetActive(true);
            }
            
            price.text = skinData.Price.ToString();
            icon.sprite = skinData.sprite;
        }

        public void ChangeBuyButtonVisibility(bool show)
        {
            buyButton.SetActive(show);
        }

        public void ChangeEquipButtonVisibility(bool show)
        {
            equipButton.SetActive(show);
        }
    }
}
