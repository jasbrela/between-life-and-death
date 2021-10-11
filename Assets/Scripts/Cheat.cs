using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayerPrefs.SetInt("allCandies", PlayerPrefs.GetInt("allCandies") + 100);
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
#endif
}
