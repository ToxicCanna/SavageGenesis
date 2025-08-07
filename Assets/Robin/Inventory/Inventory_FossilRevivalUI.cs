using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_FossilRevivalUI : MonoBehaviour
{
    private Dictionary<BaseItemStat, int> _itemsInInventory;

    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject slotPrefab;

    private void Start()
    {
        _itemsInInventory = PlayerInventory_Overworld.itemsInInventory;
        GenerateItems();
    }

    private void GenerateItems()
    {
        foreach (var itemSlot in _itemsInInventory)
        {
            GameObject newItem = Instantiate(slotPrefab, scrollContent);

            newItem.GetComponent<FossilItem>().infoText = infoText;
            newItem.GetComponent<FossilItem>().statObject = (FossilStat)itemSlot.Key;
            newItem.name = itemSlot.Key.itemName;
            newItem.GetComponent<Image>().sprite = itemSlot.Key.itemImage;
            newItem.GetComponentInChildren<TextMeshProUGUI>().text = itemSlot.Value.ToString();
        }
    }
}
