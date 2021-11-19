using Player;
using UnityEngine;

public class Cheat : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PlayerPrefs.SetInt(PlayerMoney.Key, PlayerPrefs.GetInt(PlayerMoney.Key) + 100);
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
#endif
}
