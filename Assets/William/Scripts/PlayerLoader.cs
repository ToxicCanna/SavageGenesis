using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private InventoryDinosaur slotThree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (DinoHolder.Instance.GetLength() > 0)
        {
            slotThree.GetComponent<InventoryDinosaur>().Initiate(DinoHolder.Instance.getDino(0));
        }
    }

}
