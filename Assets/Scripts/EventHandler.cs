using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private static EventHandler _instance;

    public static EventHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Events Handler");
                go.AddComponent<EventHandler>();
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

    public event Action OnGameOver;

    public void TriggerOnGameOver()
    {
        Debug.Log("OnGameOver");
        OnGameOver?.Invoke();
    } 
}