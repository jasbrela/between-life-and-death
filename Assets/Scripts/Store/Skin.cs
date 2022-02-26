using System;
using Enums;
using Player;
using Store;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable, CreateAssetMenu(fileName = "New Skin", menuName = "Store/Skin")]
    public class Skin : ScriptableObject
    {
        public SkinType type;
        public SkinCharacter character;
        public RuntimeAnimatorController animator;
        [Tooltip("For store")] public Sprite sprite;
        public Rarity rarity;

        public int Price
        {
            get
            {
                switch (rarity)
                {
                    case Rarity.Common:
                        return 2600;
                    case Rarity.Rare:
                        return 4200;
                    case Rarity.Epic:
                        return 6800;
                    case Rarity.Legendary:
                        return 11000;
                }
                
                return -1;
            }
        }
    }
}
