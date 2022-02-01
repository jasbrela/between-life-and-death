using System;
using UnityEngine;

namespace Debuff
{
    public class DebuffNotification : MonoBehaviour
    {
        private float _timer;

        private void Update()
        {
            DestroyNotificationAfterSomeTime();
        }

        private void DestroyNotificationAfterSomeTime()
        {
            _timer += Time.deltaTime;

            if (Math.Abs(_timer - 4f) < 0.25)
            {
                Destroy(gameObject);
            }
        }
    }
}
