using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Store
{
    public class SkinObject : MonoBehaviour
    {
        [SerializeField] private Skin skinData;
    
        public Skin GetSkinData => skinData;

        private void OnEnable()
        {
            bool isUnlocked = PlayerPrefs.GetInt(
                $"skin_{skinData.skinType.ToString()}_{skinData.id}") == 1;

            if (isUnlocked)
            {
                transform.Find("btn").Find("btn-buy").gameObject.SetActive(false);
                transform.Find("btn").Find("btn-equip").gameObject.SetActive(true);
            }
            transform.Find("btn").Find("btn-buy").Find("btn-text").GetComponent<TextMeshProUGUI>().text = skinData.price.ToString();
            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = skinData.sprite;
        }
    }
}
