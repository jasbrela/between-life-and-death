using System;
using System.Collections;
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

        private bool _isCandyMagnetActive;

        public bool GetIsCandyMagnetActive()
        {
            return _isCandyMagnetActive;
        }
    
        private bool _isDoubleCandiesActive;

        public bool GetIsDoubleCandiesActive()
        {
            return _isDoubleCandiesActive;
        }
    
        private bool _isNoMoreObstaclesActive;

        public bool GetIsNoMoreObstaclesActive()
        {
            return _isNoMoreObstaclesActive;
        }

        public void UseAllPowerUps()
        {
            foreach (PowerUpType powerUpType in Enum.GetValues(typeof(PowerUpType)))
            {
                UsePowerUp(powerUpType);
            }
        }

        private void UsePowerUp(PowerUpType powerUpType)
        {
            // Em que momento o jogador deve usar o power-up?
            // Por enquanto, no início da partida.
            
            // O jogador poderá usar mais de um power-up ao mesmo tempo?
            // Por enquanto, sim.
            
            // Quanto tempo cada power-up deve durar?
            // Por enquanto, todos duram 10 segundos.
            
            switch (powerUpType)
            {
                case PowerUpType.CandyMagnet:
                    if (PlayerPrefs.GetInt(powerUpType.ToString()) > 0)
                    {
                        PlayerPrefs.SetInt(powerUpType.ToString(), PlayerPrefs.GetInt(powerUpType.ToString()) - 1);
                        _isCandyMagnetActive = true;
                        StartCoroutine(StartPowerUpTimer(10, PowerUpType.CandyMagnet));
                    }

                    break;
                case PowerUpType.DoubleCandies:
                    if (PlayerPrefs.GetInt(powerUpType.ToString()) > 0)
                    {
                        PlayerPrefs.SetInt(powerUpType.ToString(), PlayerPrefs.GetInt(powerUpType.ToString()) - 1);
                        _isDoubleCandiesActive = true;
                        StartCoroutine(StartPowerUpTimer(10, PowerUpType.DoubleCandies));
                    }
                    break;
                case PowerUpType.NoMoreObstacles:
                    if (PlayerPrefs.GetInt(powerUpType.ToString()) > 0)
                    {
                        PlayerPrefs.SetInt(powerUpType.ToString(), PlayerPrefs.GetInt(powerUpType.ToString()) - 1);
                        _isNoMoreObstaclesActive = true;
                        StartCoroutine(StartPowerUpTimer(10, PowerUpType.NoMoreObstacles));
                    }
                    break;
                default:
                    throw new Exception("Tried to use an nonexistent type of Power Up: " + powerUpType);
            }
        }

        private IEnumerator StartPowerUpTimer(int seconds, PowerUpType powerUpType)
        {
            yield return new WaitForSeconds(seconds);
            switch (powerUpType)
            {
                case PowerUpType.CandyMagnet:
                    _isCandyMagnetActive = false;
                    break;
                case PowerUpType.DoubleCandies:
                    _isDoubleCandiesActive = false;
                    break;
                case PowerUpType.NoMoreObstacles:
                    _isNoMoreObstaclesActive = false;
                    break;
                default:
                    throw new Exception("Tried to start a timer of an nonexistent type of Power Up: " + powerUpType);
            }
            
        }
    }
}
