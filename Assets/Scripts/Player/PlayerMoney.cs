using System;
using ScriptableObjects;
using Store;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMoney : MonoBehaviour
    {
        public static readonly string Key = "player_money";
        [SerializeField] TMP_Text moneyLabel;
        [SerializeField] private Animation feedbackAnim;
        [SerializeField] private Animation candiesAnim;
        [SerializeField] private SpriteRenderer feedbackSprite;

        private void Start()
        {
            UpdateMoneyLabel();
            feedbackSprite.size = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * 1.5f, Screen.height * 1.5f));
        }

        private void UpdateMoneyLabel()
        {
            moneyLabel.text = PlayerPrefs.GetInt(Key, 0).ToString();
        }

        #region Skins Methods
        public void OnClickBuySkin(Button button)
        {
            Skin skinData = button.transform.parent.parent.GetComponent<SkinObject>().GetSkinData;
        
            if (PlayerPrefs.GetInt(Key) >= skinData.price)
            {
                PlayerPrefs.SetInt(Key, PlayerPrefs.GetInt(Key) - skinData.price);
                UpdateMoneyLabel();

                PlayerPrefs.SetInt($"skin_{skinData.skinType.ToString()}_{skinData.id}", 1);

                button.gameObject.SetActive(false);
                button.transform.parent.Find("btn-equip").gameObject.SetActive(true);
            }
            else
            {
                PlayNotEnoughCandiesAnimation();
            }
        }
    
        public void OnClickEquipSkin(Button button)
        {
            Skin skinData = button.transform.parent.parent.GetComponent<SkinObject>().GetSkinData;

            bool isUnlocked = PlayerPrefs.GetInt(
                $"skin_{skinData.skinType.ToString()}_{skinData.id}") == 1;
            if (isUnlocked)
            {
                PlayerPrefs.SetString($"current_{skinData.skinType.ToString()}_skin",
                    $"skin_{skinData.skinType.ToString()}_{skinData.id}");
            }
        }
        #endregion

        #region PowerUp Methods
        public void OnClickBuyPowerUp(Button button)
        {
            PowerUp powerUpData = button.transform.parent.parent.GetComponent<PowerUpObject>().GetPowerUpData;

            if (PlayerPrefs.GetInt(Key) >= powerUpData.price)
            {
                PlayerPrefs.SetInt(Key, PlayerPrefs.GetInt(Key) - powerUpData.price);
                UpdateMoneyLabel();

                PlayerPrefs.SetInt(powerUpData.powerUpType + "_level", PlayerPrefs.GetInt(powerUpData.powerUpType + "_level") + 1);
                
                if (PlayerPrefs.GetInt(powerUpData.powerUpType + "_level") == 5)
                {
                    button.gameObject.SetActive(false);
                }
            }
            else
            {
                PlayNotEnoughCandiesAnimation();
            }
        }

        private void PlayNotEnoughCandiesAnimation()
        {
            if (feedbackAnim != null) feedbackAnim.Play();
            if (candiesAnim != null) candiesAnim.Play();
        }
        
        public void OnClickWatchAdPowerUp(Button button)
        {
            throw new NotImplementedException("Ainda não foram implementados anúncios no jogo");
        }
        #endregion

    }
}
