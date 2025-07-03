using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSerialize : PropertyAttribute
{
    public string Label { get; }

    public PlayerSerialize(string label)
    {
        Label = label;
    }
}
