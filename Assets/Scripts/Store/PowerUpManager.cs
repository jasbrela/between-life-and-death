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
        public bool CanCount { get; private set; }
        
        private PowerUp _currentPowerUp;

        public delegate void OnTimerEnds();
        private OnTimerEnds _onTimerEnds;
        
        public delegate void OnUsePowerUp(PowerUpType type);
        private OnUsePowerUp _onUsePowerUp;
        
        public delegate void OnTimerStarts();
        private OnTimerStarts _onTimerStarts;

        private float count;
        
        public void UsePowerUp(PowerUp powerUp)
        {
            CanCount = true;
            _currentPowerUp = powerUp;
            ChangePowerUpStatus(powerUp.type, true);
            _onUsePowerUp?.Invoke(powerUp.type);
        }

        public void StartTimer()
        {
            if (_currentPowerUp == null || !CanCount) return;
            var type = _currentPowerUp.type;
            var duration = _currentPowerUp.durations[PlayerPrefs.GetInt(type + "_level")];
            
            StartCoroutine(StartPowerUpTimer(duration.value, type));
        }

        public void SubscribeToPowerUpTimer(OnUsePowerUp onUsePowerUp, OnTimerStarts onTimerStarts, OnTimerEnds onTimerEnds)
        {
            _onUsePowerUp += onUsePowerUp;
            _onTimerStarts += onTimerStarts;
            _onTimerEnds += onTimerEnds;
        }
        
        public void UnsubscribeToPowerUpTimer(OnUsePowerUp onUsePowerUp, OnTimerStarts onTimerStarts, OnTimerEnds onTimerEnds)
        {
            _onTimerEnds -= onTimerEnds;
            _onTimerStarts -= onTimerStarts;
            _onUsePowerUp -= onUsePowerUp;
        }

        private IEnumerator StartPowerUpTimer(float duration, PowerUpType type)
        {
            CanCount = false;
            count = duration;
            float interval = .1f;
            
            _onTimerStarts?.Invoke();
            while (count > 0)
            {
                while (PlayerStatus.isPaused || PlayerStatus.isGameOver || !Application.isFocused) yield return null;
                
                yield return new WaitForSecondsRealtime(interval);
                count -= interval;
            }
            
            _onTimerEnds?.Invoke();
            _currentPowerUp = null;
            ChangePowerUpStatus(type, false);
        }

        public float GetRemainingDuration()
        {
            return count;
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
