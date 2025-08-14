using UnityEngine;
using TMPro;

public class SlotValue : MonoBehaviour
{
    public int slotVal;

    public void SetVal(int value)
    {
        slotVal = value;

        this.gameObject.GetComponent<TextMeshProUGUI>().text = slotVal.ToString();
    }

    public int GetVal()
    {
        return slotVal;
    }    
    public void AddOne()
    {
        slotVal++;

        this.gameObject.GetComponent<TextMeshProUGUI>().text = slotVal.ToString();
    }

    public void RemoveOne()
    {
        slotVal--;
        this.gameObject.GetComponent<TextMeshProUGUI>().text = slotVal.ToString();
    }
}
