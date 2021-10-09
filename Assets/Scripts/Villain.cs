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
    private int _index;
    void Start()
    {
        PlayerStatus.CurrentDebuff = Debuff.None;
        StartCoroutine(RollDice());
    }

    IEnumerator RollDice()
    {
        int debuffsLength = Enum.GetNames(typeof(Debuff)).Length;
        
        _index = Random.Range(2, debuffsLength);
        yield return new WaitForSeconds(5);
    }

    private void Update()
    {
        switch (_index)
        {
            case 1:
                PlayerStatus.CurrentDebuff = Debuff.HigherVelocity;
                break;
            case 2:
                PlayerStatus.CurrentDebuff = Debuff.InvertedControllers;
                break;
        }
    }
}
