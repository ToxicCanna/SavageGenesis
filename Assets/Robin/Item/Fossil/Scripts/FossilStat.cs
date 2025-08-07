using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewFossil", menuName = "Scriptable Objects/Item", order = 1)]
public class FossilStat : BaseItemStat
{
    public DinosaurType fossilType;
}
