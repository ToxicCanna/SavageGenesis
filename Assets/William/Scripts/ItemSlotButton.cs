using UnityEngine;
using TMPro;

public class ItemSlotButton : MonoBehaviour
{
    private int numOfFossils;
    [SerializeField] private SlotValue numOfFossilUI;

    public void OnClick()
    {
        numOfFossils = numOfFossilUI.GetVal();
        if (numOfFossils > 0)
        {
            ReviveManager.Instance.SelectFossil(this.gameObject.GetComponent<FossilItem>().stat, numOfFossilUI);
        }
    }
}
