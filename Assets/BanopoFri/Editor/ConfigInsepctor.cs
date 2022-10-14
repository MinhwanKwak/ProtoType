using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BanpoFri
{
    [CustomEditor(typeof(Config))]
    public class ConfigInsepctor : Editor
    {
        SerializedProperty imageColorDefinesProperty;
        UnityEditorInternal.ReorderableList imageColorDefinesList;
        SerializedProperty textColorDefinesProperty;
        UnityEditorInternal.ReorderableList textColorDefinesList;
        SerializedProperty eventImageColorDefinesProperty;
        UnityEditorInternal.ReorderableList eventImageColorDefinesList;
        SerializedProperty eventTextColorDefinesProperty;
        UnityEditorInternal.ReorderableList eventTextColorDefinesList;

        public void OnEnable() 
        {
            imageColorDefinesProperty = serializedObject.FindProperty("_imageColorDefines");
            imageColorDefinesList = ReorderableListUtility.CreateAutoLayout(imageColorDefinesProperty);

            textColorDefinesProperty = serializedObject.FindProperty("_textColorDefines");
            textColorDefinesList = ReorderableListUtility.CreateAutoLayout(textColorDefinesProperty);

            eventImageColorDefinesProperty = serializedObject.FindProperty("_eventImgaeColorDefines");
            eventImageColorDefinesList = ReorderableListUtility.CreateAutoLayout(eventImageColorDefinesProperty);

            eventTextColorDefinesProperty = serializedObject.FindProperty("_eventTextColorDefines");
            eventTextColorDefinesList = ReorderableListUtility.CreateAutoLayout(eventTextColorDefinesProperty);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space();
            ReorderableListUtility.DoLayoutListWithFoldout(textColorDefinesList, "Text Color Defines");

            EditorGUILayout.Space();
            ReorderableListUtility.DoLayoutListWithFoldout(imageColorDefinesList, "Image Color Defines");

            EditorGUILayout.Space();
            ReorderableListUtility.DoLayoutListWithFoldout(eventTextColorDefinesList, "Event Text Color Defines");

            EditorGUILayout.Space();
            ReorderableListUtility.DoLayoutListWithFoldout(eventImageColorDefinesList, "Event Image Color Defines");

            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}