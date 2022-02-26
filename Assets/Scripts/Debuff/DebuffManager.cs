using System;
using System.Collections;
using Debuff;
using Player;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DebuffManager : MonoBehaviour
{
    [SerializeField] private GameObject notification;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private DebuffData reversedControls;
    [SerializeField] private DebuffData increasedSpeed;
    private int _index;
    
    void Start()
    {
        PlayerStatus.currentDebuff = DebuffType.None;
        RollDice();
    }

    private void RollDice()
    {
        int max = Enum.GetNames(typeof(DebuffType)).Length;
        _index = Random.Range(1, max);

        StartCoroutine(ShowNotification());
    }
    
    private IEnumerator ShowNotification()
    {
        if (notification != null) notification.SetActive(true);
        switch (_index)
        {
            case 1:
                PlayerStatus.currentDebuff = DebuffType.IncreasedSpeed;
                title.text = increasedSpeed.title;
                description.text = increasedSpeed.description;
                break;
            case 2:
                PlayerStatus.currentDebuff = DebuffType.ReversedControls;
                title.text = reversedControls.title;
                description.text = reversedControls.description;
                break;
            default:
                notification.SetActive(false);
                yield break;
        }
        
        notification.SetActive(true);
        
        yield return new WaitForSecondsRealtime(5);
        
        notification.SetActive(false);
    }
}
