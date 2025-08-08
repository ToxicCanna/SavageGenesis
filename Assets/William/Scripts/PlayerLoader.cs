using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    [SerializeField] private InventoryDinosaur slotThree;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slotThree = DinoHolder.Instance.getDino(0);
    }

}
