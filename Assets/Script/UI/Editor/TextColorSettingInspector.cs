using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.AutoComplete;
using UnityEditor;

[CustomEditor(typeof(TextColorSetting)), CanEditMultipleObjects]
public class TextColorSettingInspector : Editor
{
    private SerializedProperty stringPorperty; 
    private List<string> keyList = new List<string>();
    private Config loadData = null;
    private TextColorSetting textColorSetting;

    private void OnEnable()
    {
        keyList.Clear();
        loadData = AssetDatabase.LoadAssetAtPath<Config>("Assets/Arts/Config/Config.asset");
        if(loadData != null)
        {
            loadData.Load();
            foreach(var color in loadData.TextColorDefines)
                keyList.Add(color.key_string);
            foreach(var color in loadData.EventTextColorDefines) {
                keyList.Add(color.key_string);
            }
        }
        stringPorperty = serializedObject.FindProperty("keyColor");
        textColorSetting = (TextColorSetting)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        stringPorperty.stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("String Key", stringPorperty.stringValue, keyList.ToArray(), "Type something");
        if(GUILayout.Button("적용 확인")){
            textColorSetting.SetTextColor(loadData.GetTextColor(stringPorperty.stringValue));
            EditorUtility.SetDirty(target);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
