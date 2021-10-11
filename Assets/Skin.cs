using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    [SerializeField] public GameObject buyButton;
    [SerializeField] public GameObject equipButton;

    public void OnClick()
    {
        /* Se fossemos ter equip skin
        if (buyButton.activeInHierarchy)
        {
            buyButton.SetActive(false);
            equipButton.SetActive(true);
        }
        */
    }
}
