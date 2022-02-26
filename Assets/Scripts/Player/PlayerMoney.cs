using System;
using Enums;
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
        [SerializeField] private bool isDebugMode;
        [SerializeField] TMP_Text moneyLabel;
        [SerializeField] private Animation feedbackAnim;
        [SerializeField] private Animation candiesAnim;
        
        private void Start()
        {
            UpdateMoneyLabel();
        }

        private void UpdateMoneyLabel()
        {
            moneyLabel.text = PlayerPrefs.GetInt(Key, 0).ToString();
        }

        #region Skins Methods
        public void OnClickBuySkin(SkinObject skin)
        {
            Skin data = skin.SkinData;

            var price = isDebugMode ? 1 : data.Price;
            
            if (PlayerPrefs.GetInt(Key) >= price)
            {
                PlayerPrefs.SetInt(Key, PlayerPrefs.GetInt(Key) - price);
                UpdateMoneyLabel();

                PlayerPrefs.SetInt($"skin_{data.character.ToString()}_{data.type}", 1);

                skin.ChangeBuyButtonVisibility(false);
                skin.ChangeEquipButtonVisibility(true);
            }
            else
            {
                PlayNotEnoughCandiesAnimation();
            }
        }
    
        public void OnClickEquipSkin(SkinObject skin)
        {
            Skin data = skin.SkinData;
            
            bool isUnlocked = PlayerPrefs.GetInt(
                $"skin_{data.character.ToString()}_{data.type}") == 1;
                
            if (isUnlocked || data.type == SkinType.Default)
            {
                PlayerPrefs.SetString($"current_{data.character.ToString()}_skin",
                    $"skin_{data.character.ToString()}_{data.type}");
            }
        }
        #endregion

        #region PowerUp Methods
        public void OnClickBuyPowerUp(Button button)
        {
            PowerUp powerUpData = button.transform.parent.parent.GetComponent<PowerUpObject>().GetPowerUpData;

            var level = PlayerPrefs.GetInt(powerUpData.type + "_level");
            var price = isDebugMode ? 1 : powerUpData.prices[level].value;
            
            if (PlayerPrefs.GetInt(Key) >= price)
            {
                PlayerPrefs.SetInt(Key, PlayerPrefs.GetInt(Key) - price);
                UpdateMoneyLabel();
                level++;

                PlayerPrefs.SetInt(powerUpData.type + "_level", level);
                
                if (level == 5)
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
