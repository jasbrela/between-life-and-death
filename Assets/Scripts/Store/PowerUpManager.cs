using System;
using System.Collections;
using Enums;
using Player;
using UnityEngine;

namespace Store
{
    public class PowerUpManager : MonoBehaviour
    {
        #region Singleton
        private static PowerUpManager _instance;
        public static PowerUpManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("Power Up Manager");
                    go.AddComponent<PowerUpManager>();
                }
                return _instance;
            }
        }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
        }
        #endregion

        public bool IsCandyMagnetActive { get; private set; }
        public bool IsDoubleCandiesActive { get; private set; }

        public void UsePowerUp(PowerUp powerUp)
        {
            var type = powerUp.type;
            var duration = powerUp.durations[PlayerPrefs.GetInt(powerUp + "_level")];

            StartCoroutine(StartPowerUpTimer(duration.value, type));
        }

        private IEnumerator StartPowerUpTimer(float duration, PowerUpType type)
        {
            float count = 0f;
            float interval = .1f;

            ChangePowerUpStatus(type, true);
                
            while (count <= duration)
            {
                if (PlayerStatus.isPaused || PlayerStatus.isGameOver || !Application.isFocused) yield return null;
                
                yield return new WaitForSecondsRealtime(interval);
                count += interval;
            }
            
            ChangePowerUpStatus(type, false);
        }

        private void ChangePowerUpStatus(PowerUpType type, bool status)
        {
            switch (type)
            {
                case PowerUpType.CandyMagnet:
                    IsCandyMagnetActive = status;
                    break;
                
                case PowerUpType.DoubleCandies:
                    IsDoubleCandiesActive = status;
                    break;

                default:
                    throw new Exception("Tried to change the status of an nonexistent type of Power Up: " + type);
            }
        }

        public PowerUpType IsAnyPowerUpActive()
        {
            if (IsCandyMagnetActive) return PowerUpType.CandyMagnet;
            if (IsDoubleCandiesActive) return PowerUpType.DoubleCandies;
            return PowerUpType.None;
        }
    }
}
