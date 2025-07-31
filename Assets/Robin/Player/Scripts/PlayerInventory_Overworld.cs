using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory_Overworld
{
    //Inventory as a dictionary
    //To Killian: since we only have single player for now then set the inventory to static, tell me if you want to make multi-player

    public static Dictionary<BaseItem, int> itemsInInventory = new Dictionary<BaseItem, int>();

    public static void DebugDisplayAllItemsInInventory()
    {
        if (itemsInInventory.Count == 0)
        {
            Debug.Log("You have nothing in your inventory.");
        }
        else
        {
            foreach (var inventorySlot in itemsInInventory)
                Debug.Log($"You have {inventorySlot.Value} of {inventorySlot.Key.statObject.itemName}s in your inventory.");
        }
    }
    
    public static void CollectItem(BaseItem item, int collectValue)
    {
        itemsInInventory.Add(item, collectValue);

        Debug.Log($"You get a {item.statObject.itemName}!");
    }

    public static void DiscardItem(BaseItem item, int discardValue)
    {
        if (!itemsInInventory.ContainsKey(item))
        {
            Debug.Log("Item not found");
            return;
        }
        
        if (itemsInInventory[item] <= discardValue)
            itemsInInventory.Remove(item);
        else
            itemsInInventory[item] -= discardValue;

        Debug.Log($"Item '{item}' has been discarded!");
    }

    public static void ClearInventory()
    {
        itemsInInventory.Clear();
        Debug.Log("Inventory data is successfully deleted.");
    }
}
