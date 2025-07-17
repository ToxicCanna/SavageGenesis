using UnityEngine;

[System.Serializable]
public class FossilLoot
{
    public Fossil fossil;
    [Min(1)]
    public int maxFossilNumber;
}
