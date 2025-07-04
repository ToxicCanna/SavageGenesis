using UnityEngine;

public abstract class BaseItemStat : ScriptableObject
{
    public string itemName;

    [Min(0f)]
    public float price;
}
