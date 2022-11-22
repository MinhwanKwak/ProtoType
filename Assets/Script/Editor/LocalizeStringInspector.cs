using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.AutoComplete;
using UnityEditor;

[CustomEditor(typeof(LocalizeString))]
public class LocalizeStringInspector : Editor
{
    private SerializedProperty stringPorperty; 
    private List<string> keyList = new List<string>();
    private BanpoFri.Localize loadData = null;

    private void OnEnable()
    {
        keyList.Clear();
        loadData = AssetDatabase.LoadAssetAtPath<BanpoFri.Localize>("Assets/BanpoFri/TableAsset/Localize.asset");
        if(loadData != null)
        {
            foreach(var data in loadData.DataList)
                keyList.Add(data.idx);
        }
        stringPorperty = serializedObject.FindProperty("keyLocalize");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        var prevStrValue = stringPorperty.stringValue;
        stringPorperty.stringValue = AutoCompleteTextField.EditorGUILayout.AutoCompleteTextField("String Key", stringPorperty.stringValue, keyList.ToArray(), "Type something");
        if(stringPorperty.stringValue != prevStrValue){
            (serializedObject.targetObject as LocalizeString).SetText(
                loadData.DataList.Find(x => x.idx == stringPorperty.stringValue).en);
            EditorUtility.SetDirty(target);
        }
        serializedObject.ApplyModifiedProperties();        
    }
}
