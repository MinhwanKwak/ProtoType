using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.AutoComplete;
using UnityEditor;

[CustomEditor(typeof(ProgressText)), CanEditMultipleObjects]
public class ProgressTextInspector : Editor
{
    private SerializedProperty frontStringPorperty;
    private SerializedProperty backStringPorperty;
    private SerializedProperty fullStringPorperty;
    private SerializedProperty fullBoolProperty;
    private List<string> keyList = new List<string>();
    private Config loadData = null;
    private ProgressText progressText;

    private void OnEnable()
    {
        keyList.Clear();
        loadData = AssetDatabase.LoadAssetAtPath<Config>("Assets/Arts/Config/Config.asset");
        if(loadData != null)
        {
            loadData.Load();
            foreach(var color in loadData.TextColorDefines)
                keyList.Add(color.key_string);
        }
        frontStringPorperty = serializedObject.FindProperty("frontTextColorKey");
        backStringPorperty = serializedObject.FindProperty("backTextColorKey");
        fullStringPorperty = serializedObject.FindProperty("fullTextColorKey");
        fullBoolProperty = serializedObject.FindProperty("FullColorPossible");
        progressText = (ProgressText)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginVertical();
        frontStringPorperty.stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("Front Color Key", frontStringPorperty.stringValue, keyList.ToArray(), "Type something");
        EditorGUILayout.ColorField(loadData.GetTextColor(frontStringPorperty.stringValue));
        backStringPorperty.stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("Back Color Key", backStringPorperty.stringValue, keyList.ToArray(), "Type something");
        EditorGUILayout.ColorField(loadData.GetTextColor(backStringPorperty.stringValue));
        fullBoolProperty.boolValue = EditorGUILayout.Toggle("Full Possible", fullBoolProperty.boolValue);
        if(fullBoolProperty.boolValue)
        {
            fullStringPorperty.stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("Full Color Key", fullStringPorperty.stringValue, keyList.ToArray(), "Type something");
            EditorGUILayout.ColorField(loadData.GetTextColor(fullStringPorperty.stringValue));
        }
        else
            fullStringPorperty.stringValue = string.Empty;
        
        if(GUILayout.Button("적용 확인")){
            EditorUtility.SetDirty(target);
            progressText.Test();            
        }
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
