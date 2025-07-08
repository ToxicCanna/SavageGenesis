using System;
using UnityEngine;

public abstract class BaseItemStat : ScriptableObject
{
    public string itemName;
    [Min(0)]
    public float price;

    public RarityLevel rarity;
}