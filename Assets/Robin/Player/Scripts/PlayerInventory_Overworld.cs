using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory_Overworld
{
    //Inventory as a dictionary
    //To Killian: since we only have single player for now then set the inventory to static, tell me if you want to make multi-player
    //collectValue and discardValue denotes how many fossils at a time 

    public static Dictionary<BaseItemStat, int> itemsInInventory = new Dictionary<BaseItemStat, int>();

    public static void DebugDisplayAllItemsInInventory()
    {
        if (itemsInInventory.Count == 0)
        {
            Debug.Log("You have nothing in your inventory.");
        }
        else
        {
            foreach (var inventorySlot in itemsInInventory)
            {
                Debug.Log
                (
                    $"You have {inventorySlot.Value} of {inventorySlot.Key.itemName}s in your inventory, " +
                    $"and the rarity is {inventorySlot.Key.rarity}."
                );
            }
                
        }
    }
    
    public static void CollectItem(BaseItemStat itemStat, int collectValue)
    {
        if (!itemsInInventory.TryAdd(itemStat, collectValue))
        {
            itemsInInventory[itemStat] += collectValue;
        }

        Debug.Log($"You get {collectValue} of {itemStat.itemName}s!");
    }

    public static void DiscardItem(BaseItemStat itemStat, int discardValue)
    {
        if (!itemsInInventory.ContainsKey(itemStat))
        {
            Debug.Log("Item not found");
            return;
        }
        
        if (itemsInInventory[itemStat] <= discardValue)
            itemsInInventory.Remove(itemStat);
        else
            itemsInInventory[itemStat] -= discardValue;

        Debug.Log($"Item '{itemStat.itemName}' has been discarded!");
    }

    public static void ClearInventory()
    {
        itemsInInventory.Clear();
        Debug.Log("Inventory data is successfully deleted.");
    }
}
