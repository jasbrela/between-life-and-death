using ScriptableObjects;
using Store;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private Skin defaultHumanSkin;
    [SerializeField] private Skin defaultGhostSkin;
    [SerializeField] private AllSkins allSkins;

    private void Start()
    {
        SkinManager.Instance.SetAllSkins(allSkins);
        SkinManager.Instance.SetLastSelectedSkin(defaultHumanSkin);
        SkinManager.Instance.SetLastSelectedSkin(defaultGhostSkin);

        if (PlayerPrefs.GetInt("FirstEntry") == 1)
        {
            SkinManager.Instance.FindSelectedSkinData(gameObject);
        }
        else
        {
            PlayerPrefs.SetInt("FirstEntry", 1);
            SkinManager.Instance.SetDefaultSkin(gameObject);
        }
    }
}
