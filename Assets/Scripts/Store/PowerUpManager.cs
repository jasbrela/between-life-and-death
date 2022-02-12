using System;
using System.Collections;
using System.Text;
using Enums;
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

            StartCoroutine(StartPowerUpTimer(duration, type));
        }

        private IEnumerator StartPowerUpTimer(float seconds, PowerUpType type)
        {
            switch (type)
            {
                case PowerUpType.CandyMagnet:
                    Debug.Log("candyMagnet - " + IsCandyMagnetActive);
                    IsCandyMagnetActive = true;
                    yield return new WaitForSecondsRealtime(seconds);
                    IsCandyMagnetActive = false;
                    break;
                
                case PowerUpType.DoubleCandies:
                    IsDoubleCandiesActive = true;
                    yield return new WaitForSecondsRealtime(seconds);
                    IsDoubleCandiesActive = false;
                    break;

                default:
                    throw new Exception("Tried to use an nonexistent type of Power Up: " + type);
            }
        }
    }
}
