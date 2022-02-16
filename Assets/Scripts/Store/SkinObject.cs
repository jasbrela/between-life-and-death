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
            if (skinData.sprite == null) gameObject.SetActive(false);
            
            bool isUnlocked = PlayerPrefs.GetInt(
                $"skin_{skinData.character.ToString()}_{skinData.type}") == 1;

            if (isUnlocked || skinData.type == SkinType.Default)
            {
                buyButton.SetActive(false);
                equipButton.SetActive(true);
            }
            price.text = skinData.price.ToString();
            icon.sprite = skinData.sprite;
        }
    }
}
