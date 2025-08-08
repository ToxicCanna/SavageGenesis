using System.Collections.Generic;
using UnityEngine;

public class DinoHolder : Code.Scripts.Managers.Singleton<DinoHolder>
{
    [SerializeField]public List<InventoryDinosaur> DinoBag;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DinoBag = new List<InventoryDinosaur>();
    }

    public void addDino(InventoryDinosaur dino)
    {
        DinoBag.Add(dino);
    }

    public void removeDinoAt(int index)
    {
        DinoBag.RemoveAt(index);
    }

    public InventoryDinosaur getDino(int index)
    {
        return DinoBag[index];
    }
}
