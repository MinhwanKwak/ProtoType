using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;

namespace BanpoFri
{
    [CustomEditor(typeof(UIBase), true)]
    public class UIBaseInspector : Editor
    {        
        private string startAniStateName = "Show";
        private List<string> stateNames = new List<string>();
        private int selectIdx = 0;

        private void OnEnable() {
            stateNames.Clear();
            foreach(var iter in typeof(MonoBehaviour).GetMethods().Where( x => x.Name == "GetComponent"))
            {
                var animator = iter.Invoke(target, new object[1] {typeof(Animator)}) as Animator;
                if(animator != null)
                {
                    if(animator.runtimeAnimatorController == null)
                        return;

                    var controller = animator.runtimeAnimatorController as AnimatorController;
                    foreach(var state in controller.layers[0].stateMachine.states)
                    {
                        if(state.state.name.StartsWith(startAniStateName))
                        {
                            stateNames.Add(state.state.name);
                        }
                    }
                }
                break;
            }
            if(string.IsNullOrEmpty(serializedObject.FindProperty("uiShowStateName").stringValue) &&
                stateNames.Count > 0)
            {
                serializedObject.Update();
                serializedObject.FindProperty("uiShowStateName").stringValue = stateNames.FirstOrDefault();
                serializedObject.ApplyModifiedProperties();
            }   
            
            var value = serializedObject.FindProperty("uiShowStateName").stringValue;
            selectIdx = stateNames.FindIndex(x => x == value);
            if(selectIdx < 0)
                selectIdx = 0;
        }

        public override void OnInspectorGUI()
        {
            if(stateNames.Count > 0)
            {
                int prevIdx = selectIdx;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("ShowAniStateName");
                selectIdx = EditorGUILayout.Popup(selectIdx, stateNames.ToArray());
                EditorGUILayout.EndHorizontal();
                if(prevIdx != selectIdx)
                {  
                    serializedObject.Update();
                    serializedObject.FindProperty("uiShowStateName").stringValue = stateNames[selectIdx];
                    serializedObject.ApplyModifiedProperties();
                }
            }
            base.OnInspectorGUI();
        }
    }
}
