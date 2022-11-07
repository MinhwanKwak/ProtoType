using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.AutoComplete;
using UnityEditor;

[CustomEditor(typeof(ClickCallback))]
public class ClickCallbackInspector : Editor
{
    private SerializedProperty stringPorperty; 
    private List<string> keyList = new List<string>();

    private void OnEnable()
    {
        keyList.Clear();
        keyList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
        stringPorperty = serializedObject.FindProperty("targetTagName");
    }

    public override void OnInspectorGUI()
    {        
        serializedObject.Update();
        stringPorperty.stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("Target Tag", stringPorperty.stringValue, keyList.ToArray(), "Type something");
        serializedObject.ApplyModifiedProperties();
        base.OnInspectorGUI();
    }
}
