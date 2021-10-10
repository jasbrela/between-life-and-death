using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffNotification : MonoBehaviour
{
    public static bool ActivateNotification;
    private float _timer;

    private void OnEnable()
    {
        ActivateNotification = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (Math.Abs(_timer - 4f) < 0.25)
        {
            ActivateNotification = false;
            gameObject.SetActive(false);
        }
    }
}
