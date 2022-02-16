using System;
using Enums;
using Player;
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
            foreach (SkinCharacter skinType in Enum.GetValues(typeof(SkinCharacter)))
            {
                PlayerPrefs.SetString($"current_{skinType.ToString()}_skin",
                    $"skin_{skinType.ToString()}_{SkinType.Default.ToString()}");
            }

            UpdateAnimator(player);
        }

        /// <summary>
        /// This will find the Skin Type and ID formerly selected by player, then set the skin.
        /// </summary>
        public void FindSelectedSkinData(GameObject player)
        {
            SkinCharacter skinCharacter = PlayerStatus.isGhostMode ? SkinCharacter.Ghost : SkinCharacter.Human;
            
            string s = PlayerPrefs.GetString($"current_{skinCharacter.ToString()}_skin")
                .Replace("skin_", string.Empty);
            
            int underlineIndex = s.IndexOf("_");

            string cType = s.Substring(0, underlineIndex);
            string cId = s.Substring(underlineIndex + 1);
            
            SetLastSelectedSkin((SkinCharacter) Enum.Parse(typeof(SkinCharacter), cType, true),
                (SkinType) Enum.Parse(typeof(SkinType), cId, true));

            UpdateAnimator(player);
        }

        /// <summary>
        /// This will update the animation of Player Skin's Animator
        /// </summary>
        private void UpdateAnimator(GameObject player)
        {
            player.transform.GetComponent<Animator>().runtimeAnimatorController
                = PlayerStatus.isGhostMode
                    ? _lastSelectedGhostSkin.animator
                    : _lastSelectedHumanSkin.animator;
        }

        /// <param name="skin">Full data of skin part</param>
        public void SetLastSelectedSkin(Skin skin)
        {
            switch (skin.character)
            {
                case SkinCharacter.Human:
                    _lastSelectedHumanSkin = skin;
                    break;
                case SkinCharacter.Ghost:
                    _lastSelectedGhostSkin = skin;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <param name="skinCharacter">The type of the skin</param>
        /// <param name="type">The type of the skin</param>
        private void SetLastSelectedSkin(SkinCharacter skinCharacter, SkinType type)
        {
            switch (skinCharacter)
            {
                case SkinCharacter.Human:
                    try
                    {
                        FindSkin(_allSkins.humanSkins, SkinCharacter.Human, type);
                    }
                    catch
                    {
                        throw new Exception(
                            $"You probably forgot to add this skin ({SkinCharacter.Human}_{type}) to AllSkins object.");
                    }

                    break;
                case SkinCharacter.Ghost:
                    try
                    {
                        FindSkin(_allSkins.ghostSkins, SkinCharacter.Ghost, type);
                    }
                    catch
                    {
                        throw new Exception(
                            $"You probably forgot to add this skin ({SkinCharacter.Ghost}_{type}) to AllSkins object.");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skinCharacter), skinCharacter, null);
            }
        }

        /// <summary>
        /// This will find the skin and then set it.
        /// </summary>
        /// <param name="skins">Skin[] from AllSkins equivalent to the type of the skin</param>
        /// <param name="skinCharacter">The type of the clothes</param>
        /// <param name="type">The type of the skin</param>
        private void FindSkin(Skin[] skins, SkinCharacter skinCharacter, SkinType type)
        {
            if (skins == null) return;
            foreach (Skin skin in skins)
            {
                if (skin.type == type)
                {
                    switch (skinCharacter)
                    {
                        case SkinCharacter.Human:
                            _lastSelectedHumanSkin = skin;
                            break;
                        case SkinCharacter.Ghost:
                            _lastSelectedGhostSkin = skin;
                            break;
                    }
                }
            }
        }
    }
}