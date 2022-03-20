using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(NullableVar<float>))]
public class NullableVarDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var serializedValue = property.FindPropertyRelative("Value");
        var serializedIsNullValue = property.FindPropertyRelative("IsNull");

        const float rectBoolSize = 20;
        var rectValue = new Rect(position.x + rectBoolSize, position.y, 150, position.height);
        var rectBool = new Rect(position.x, position.y, rectBoolSize, position.height);

        EditorGUI.PropertyField(rectBool, serializedIsNullValue, GUIContent.none);
        GUI.enabled = !serializedIsNullValue.boolValue;
        EditorGUI.PropertyField(rectValue, serializedValue, GUIContent.none);
        GUI.enabled = true;

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
