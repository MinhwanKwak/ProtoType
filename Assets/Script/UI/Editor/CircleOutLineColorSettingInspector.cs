using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.AutoComplete;
using UnityEditor;
using TextOutline;

[CustomEditor(typeof(CircleOutline)), CanEditMultipleObjects]
public class CircleOutlineInspector : Editor
{
    private SerializedProperty stringPorperty;
    private List<string> keyList = new List<string>();
    private Config loadData = null;

    private void OnEnable()
    {
        keyList.Clear();
        loadData = AssetDatabase.LoadAssetAtPath<Config>("Assets/Arts/Config/Config.asset");
        if (loadData != null)
        {
            loadData.Load();
            foreach (var color in loadData.TextColorDefines)
                keyList.Add(color.key_string);
        }
        stringPorperty = serializedObject.FindProperty("keyColor");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        stringPorperty.stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("String Key", stringPorperty.stringValue, keyList.ToArray(), "Type something");
        if (GUILayout.Button("컬러 적용"))
        {
            foreach (var target in Selection.gameObjects)
            {
                target.GetComponent<CircleOutline>().effectColor = loadData.GetTextColor(stringPorperty.stringValue);
                EditorUtility.SetDirty(target);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
