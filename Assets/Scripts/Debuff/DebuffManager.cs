using System;
using System.Collections;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class DebuffManager : MonoBehaviour
{
    [SerializeField] private GameObject notification;
    [SerializeField] private GameObject invertedNotification;
    [SerializeField] private GameObject speedNotification;
    private int _index;
    
    void Start()
    {
        PlayerStatus.currentDebuff = DebuffType.None;
        StartCoroutine(RollDice());
    }

    IEnumerator RollDice()
    {
        int debuffsLength = Enum.GetNames(typeof(DebuffType)).Length;
        
        _index = Random.Range(1, debuffsLength);
        yield return new WaitForSeconds(5);
    }

    private void Update()
    {
        switch (_index)
        {
            case 1:
                PlayerStatus.currentDebuff = DebuffType.HigherVelocity;
                if (notification != null && speedNotification != null && invertedNotification != null)
                {
                    notification.SetActive(true);
                    speedNotification.SetActive(true);
                    invertedNotification.SetActive(false);
                }

                break;
            case 2:
                PlayerStatus.currentDebuff = DebuffType.InvertedControllers;
                if (notification != null && speedNotification != null && invertedNotification != null)
                {
                    notification.SetActive(true);
                    speedNotification.SetActive(false);
                    invertedNotification.SetActive(true);
                }
                break;
        }
        
        if (PlayerStatus.currentDebuff == DebuffType.None)
        {
            notification.SetActive(false);
        }
    }
}
