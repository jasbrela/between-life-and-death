using System;
using Enums;
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
        public int price;
    }
}
