using UnityEngine;

// Define the CustomLabelAttribute class
public class CustomLabelAttribute : PropertyAttribute
{
    public string Label { get; }

    public CustomLabelAttribute(string label)
    {
        Label = label;
    }
}