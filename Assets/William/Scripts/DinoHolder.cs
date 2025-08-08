using System.Collections.Generic;
using UnityEngine;

public class DinoHolder : Code.Scripts.Managers.Singleton<DinoHolder>
{
    [SerializeField]public List<InventoryDinosaur> DinoBag;
    public InventoryDinosaur[] ForDebug;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DinoBag = new List<InventoryDinosaur>();
        ForDebug = new InventoryDinosaur[5];
    }

    public void addDino(InventoryDinosaur dino)
    {
        DinoBag.Add(dino);
        ForDebug[0] = dino;
    }

    public void removeDinoAt(int index)
    {
        DinoBag.RemoveAt(index);
    }

    public InventoryDinosaur getDino(int index)
    {
        return ForDebug[0];
        //return DinoBag[index];
    }
}
