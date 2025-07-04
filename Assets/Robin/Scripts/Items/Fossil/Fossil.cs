using UnityEngine;

public class Fossil : BaseItem
{
    public void GetFossil()
    {
        Debug.Log($"Hey, I got {statObject.itemName}!");
    }
}
