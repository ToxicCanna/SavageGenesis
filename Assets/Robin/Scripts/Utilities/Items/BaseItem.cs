using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseItem : MonoBehaviour, IShop, IPointerEnterHandler, IPointerExitHandler
{
    public BaseItemStat statObject;
    public TextMeshProUGUI infoText;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        infoText.text = 
            $"Name: {statObject.itemName}" +
            $"\nRarity: {statObject.rarity}";
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        infoText.text = "";
    }

    public virtual void OnBuying()
    {
        if (statObject != null) 
        {
            Debug.Log($"Hey, I bought {statObject.itemName} for {statObject.price}!");
        }
        else
            Debug.Log("Stat object is null!");
    }

    public virtual void OnSelling(float sellRate)
    {
        if (statObject != null)
        {
            var sellValue = Mathf.Round(statObject.price * sellRate / 100.0f);
            Debug.Log($"Hey, I sold {statObject.itemName} for {sellValue}!");
        }
        else
            Debug.Log("Stat object is null!");
    }
}
