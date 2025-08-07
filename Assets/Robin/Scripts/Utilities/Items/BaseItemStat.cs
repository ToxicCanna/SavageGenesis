using System;
using UnityEngine;

public abstract class BaseItemStat : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    
    [Min(0)]
    public float price;
    [NonSerialized] public RarityLevel rarity;
}