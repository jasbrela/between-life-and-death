using System;
using Enums;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "New PowerUp", menuName = "Store/Power Up")]
public class PowerUp : ScriptableObject
{
    public PowerUpType type;
    [Tooltip("For store")] public Sprite sprite;
    public IntegerVariable[] prices;
    public FloatVariable[] durations;
}