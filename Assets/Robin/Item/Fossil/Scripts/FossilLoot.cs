using UnityEngine;

[System.Serializable]
public class FossilLoot
{
    public Fossil fossil;
    [Min(1)]
    public int lootNumber;

    public FossilLoot()
    {
        if (lootNumber <= 0)
            lootNumber = 1;
    }
}
