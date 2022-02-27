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
        [SerializeField] private Animator borderAnimator;
        [SerializeField] private Animator candyAnimator;
        
        private static readonly int NotEnoughCandy = Animator.StringToHash("NotEnoughCandy");

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
                Debug.Log("entra aqui");
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
        public void OnClickBuyPowerUp(PowerUpObject obj)
        {
            PowerUp data = obj.GetPowerUpData;

            var level = PlayerPrefs.GetInt(data.type + "_level");
            var price = isDebugMode ? 1 : data.prices[level].value;
            
            if (PlayerPrefs.GetInt(Key) >= price)
            {
                PlayerPrefs.SetInt(Key, PlayerPrefs.GetInt(Key) - price);
                UpdateMoneyLabel();
                level++;

                PlayerPrefs.SetInt(data.type + "_level", level);
            }
            else
            { 
                PlayNotEnoughCandiesAnimation();
            }
        }

        private void PlayNotEnoughCandiesAnimation()
        {
            borderAnimator.SetTrigger(NotEnoughCandy);
            candyAnimator.SetTrigger(NotEnoughCandy);
        }
        
        public void OnClickWatchAdPowerUp(Button button)
        {
            throw new NotImplementedException("Ainda não foram implementados anúncios no jogo");
        }
        #endregion

    }
}
