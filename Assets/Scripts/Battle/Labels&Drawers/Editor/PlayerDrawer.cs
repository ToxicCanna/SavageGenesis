using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PlayerSerialize))]
public class PlayerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        CustomLabelAttribute customLabelAttribute = attribute as CustomLabelAttribute;

        // Display a custom label instead of the default property name
        GUIContent customLabel = new GUIContent("Player");

        // Draw the property field with the custom label
        EditorGUI.PropertyField(position, property, customLabel);

        EditorGUI.EndProperty();
    }
}
