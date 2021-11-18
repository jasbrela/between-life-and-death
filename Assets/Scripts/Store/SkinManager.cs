using System;
using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Store
{
    public class SkinManager : MonoBehaviour
    {
        #region Singleton

        private static SkinManager _instance;

        public static SkinManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("Skin Manager");
                    go.AddComponent<SkinManager>();
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
        #endregion

        private Skin _lastSelectedHumanSkin;
        private Skin _lastSelectedGhostSkin;
        private AllSkins _allSkins;

        public void SetAllSkins(AllSkins allSkins)
        {
            _allSkins = allSkins;
        }
        
        /// <summary>
        /// This will set the default skin as current skin, then set the skin.
        /// </summary>
        public void SetDefaultSkin(GameObject player)
        {
            foreach (SkinType skinType in Enum.GetValues(typeof(SkinType)))
            {
                PlayerPrefs.SetString($"current_{skinType.ToString()}_skin",
                    $"skin_{skinType.ToString()}_{SkinID.Default.ToString()}");
            }

            UpdateAnimator(player);
        }

        /// <summary>
        /// This will find the Skin Type and ID formerly selected by player, then set the skin.
        /// </summary>
        public void FindSelectedSkinData(GameObject player)
        {
            SkinType skinType = PlayerStatus.GhostMode ? SkinType.Ghost : SkinType.Human;
            
            string s = PlayerPrefs.GetString($"current_{skinType.ToString()}_skin")
                .Replace("skin_", string.Empty);
            
            int underlineIndex = s.IndexOf("_");

            string cType = s.Substring(0, underlineIndex);
            string cId = s.Substring(underlineIndex + 1);
            
            SetLastSelectedSkin((SkinType) Enum.Parse(typeof(SkinType), cType, true),
                (SkinID) Enum.Parse(typeof(SkinID), cId, true));

            UpdateAnimator(player);
        }

        /// <summary>
        /// This will update the animation of Player Skin's Animator
        /// </summary>
        private void UpdateAnimator(GameObject player)
        {
            player.transform.GetComponent<Animator>().runtimeAnimatorController
                = PlayerStatus.GhostMode
                    ? _lastSelectedGhostSkin.animator
                    : _lastSelectedHumanSkin.animator;
        }

        /// <param name="skin">Full data of skin part</param>
        public void SetLastSelectedSkin(Skin skin)
        {
            switch (skin.skinType)
            {
                case SkinType.Human:
                    _lastSelectedHumanSkin = skin;
                    break;
                case SkinType.Ghost:
                    _lastSelectedGhostSkin = skin;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <param name="skinType">The type of the skin</param>
        /// <param name="id">The id of the skin</param>
        private void SetLastSelectedSkin(SkinType skinType, SkinID id)
        {
            switch (skinType)
            {
                case SkinType.Human:
                    try
                    {
                        FindSkin(_allSkins.humanSkins, SkinType.Human, id);
                    }
                    catch
                    {
                        throw new Exception(
                            $"You probably forgot to add this skin ({SkinType.Human}_{id}) to AllSkins object.");
                    }

                    break;
                case SkinType.Ghost:
                    try
                    {
                        FindSkin(_allSkins.ghostSkins, SkinType.Ghost, id);
                    }
                    catch
                    {
                        throw new Exception(
                            $"You probably forgot to add this skin ({SkinType.Ghost}_{id}) to AllSkins object.");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skinType), skinType, null);
            }
        }

        /// <summary>
        /// This will find the skin and then set it.
        /// </summary>
        /// <param name="skins">Skin[] from AllSkins equivalent to the type of the skin</param>
        /// <param name="skinType">The type of the clothes</param>
        /// <param name="id">The id of the skin</param>
        private void FindSkin(Skin[] skins, SkinType skinType, SkinID id)
        {
            if (skins == null) return;
            foreach (Skin skin in skins)
            {
                if (skin.id == id)
                {
                    switch (skinType)
                    {
                        case SkinType.Human:
                            _lastSelectedHumanSkin = skin;
                            break;
                        case SkinType.Ghost:
                            _lastSelectedGhostSkin = skin;
                            break;
                    }
                }
            }
        }
    }
}