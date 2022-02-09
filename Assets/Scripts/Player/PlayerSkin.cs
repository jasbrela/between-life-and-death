using ScriptableObjects;
using Store;
using UnityEngine;

namespace Player
{
    public class PlayerSkin : MonoBehaviour
    {
        private const string FirstEntryKey = "FirstEntry";

        [SerializeField] private GameObject sprite;
        [SerializeField] private Skin defaultHumanSkin;
        [SerializeField] private Skin defaultGhostSkin;
        [SerializeField] private AllSkins allSkins;

        private void Start()
        {
            SkinManager.Instance.SetAllSkins(allSkins);
            SkinManager.Instance.SetLastSelectedSkin(defaultHumanSkin);
            SkinManager.Instance.SetLastSelectedSkin(defaultGhostSkin);

            if (PlayerPrefs.GetInt(FirstEntryKey) == 1)
            {
                SkinManager.Instance.FindSelectedSkinData(sprite);
            }
            else
            {
                PlayerPrefs.SetInt(FirstEntryKey, 1);
                SkinManager.Instance.SetDefaultSkin(sprite);
            }
        }
    }
}
