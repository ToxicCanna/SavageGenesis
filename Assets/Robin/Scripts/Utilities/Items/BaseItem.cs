using UnityEngine;

public abstract class BaseItem : MonoBehaviour, IShop
{
    //Put your correct stat object here, do not get messed up like a healing item with fossil stat
    public BaseItemStat statObject;

    public void OnBuying()
    {
        if (statObject != null) 
        {
            Debug.Log($"Hey, I bought {statObject.itemName} for {statObject.price}!");
        }
        else
            Debug.Log("Stat object is null!");
    }

    public void OnSelling(float sellRate)
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
