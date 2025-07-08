//To Killian: what list do we have for a loot

using UnityEngine;

[System.Serializable]
public class FossilLoot
{
    //Fossil Object
    public Fossil fossil;

    #region Loot Structure
    [Range(0, 99)]
    public int itemNumber = 1;
    [Range(0, 99)]
    public int lootValue = 1;
    #endregion
}
