using UnityEngine;

public class Fossil : BaseItem<FossilStat>
{
    public void GetFossil()
    {
        Debug.Log($"Hey, I got {statObject.itemName}!");
    }
}
