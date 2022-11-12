using UnityEngine;
using UnityEditor;

namespace BanpoFri
{
    [CustomEditor(typeof(Table<,>), true)]
    public class TableInspector : Editor
    {
        UnityEditorInternal.ReorderableList mDataList;

        public void OnEnable()
        {
            var property = serializedObject.FindProperty("mDataList");
            mDataList = ReorderableListUtility.CreateAutoLayout(property);
            mDataList.serializedProperty.isExpanded = false;
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //EditorGUILayout.PropertyField(serializedObject.FindProperty("mDataList"), true);
            ReorderableListUtility.DoLayoutListWithFoldout(mDataList, "Datas");
            if(GUILayout.Button("테이블 수정")){
                TableEditWindow.ShowUI((BanpoFri.Table)this.serializedObject.targetObject);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
