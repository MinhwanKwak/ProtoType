using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.AutoComplete;
using UnityEditor;

[CustomEditor(typeof(ImageColorSetting)), CanEditMultipleObjects]
public class ImageColorSettingInspector : Editor
{
    private SerializedProperty stringProperty;
    private SerializedProperty string2Property;
    private List<string> keyList = new List<string>();
    private Config loadData = null;

    private ImageColorSetting[] targetSctripts;

    private SerializedProperty eventCountProperty;

    private void OnEnable() 
    {
        keyList.Clear();
        
        loadData = AssetDatabase.LoadAssetAtPath<Config>("Assets/Arts/Config/Config.asset");
        if(loadData != null) {
            loadData.Load();
            foreach(var color in loadData.ImageColorDefines) {
                keyList.Add(color.key_string);
            }
            foreach(var color in loadData.EventImageColorDefines) {
                keyList.Add(color.key_string);
            }
        }

        stringProperty = serializedObject.FindProperty("keyColor");
        string2Property = serializedObject.FindProperty("eventKeyColor");
        eventCountProperty = serializedObject.FindProperty("eventCount");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        
        stringProperty.stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("String Key", stringProperty.stringValue, keyList.ToArray(), "Type something");

        if(serializedObject.FindProperty("SetEventMode").boolValue == true)
        {
            eventCountProperty.intValue = EditorGUILayout.IntField("EventCount:", eventCountProperty.intValue);

            for(int i = 0; i < eventCountProperty.intValue; i++)
            {
                if(string2Property.arraySize <= i)
                    string2Property.InsertArrayElementAtIndex(i);
                string2Property.GetArrayElementAtIndex(i).stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField($"String Event{(i+1)} Key", string2Property.GetArrayElementAtIndex(i).stringValue, keyList.ToArray(), "Type something");
            }
        }
        if (GUILayout.Button("적용 확인")) {
            foreach(var target in Selection.gameObjects) {
                target.GetComponent<ImageColorSetting>().SetImageColor(loadData.GetImageColor(stringProperty.stringValue));
                EditorUtility.SetDirty(target);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
