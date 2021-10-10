using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Debuff
{
    None,
    HigherVelocity,
    InvertedControllers
}

public class Villain : MonoBehaviour
{
    [SerializeField] private GameObject notif;
    [SerializeField] private GameObject invertedNotif;
    [SerializeField] private GameObject speedNotif;
    private int _index;
    void Start()
    {
        PlayerStatus.CurrentDebuff = Debuff.None;
        StartCoroutine(RollDice());
    }

    IEnumerator RollDice()
    {
        int debuffsLength = Enum.GetNames(typeof(Debuff)).Length;
        
        _index = Random.Range(1, debuffsLength);
        yield return new WaitForSeconds(5);
    }

    private void Update()
    {
        switch (_index)
        {
            case 1:
                PlayerStatus.CurrentDebuff = Debuff.HigherVelocity;
                notif.SetActive(true);
                speedNotif.SetActive(true);
                invertedNotif.SetActive(false);
                break;
            case 2:
                notif.SetActive(true);
                speedNotif.SetActive(false);
                invertedNotif.SetActive(true);
                PlayerStatus.CurrentDebuff = Debuff.InvertedControllers;
                break;
        }
        
        if (PlayerStatus.CurrentDebuff == Debuff.None)
        {
            notif.SetActive(false);
        }
    }
}
