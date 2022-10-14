using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace BanpoFri
{
    [System.Serializable]
    public class SampleTableData
    {
        [SerializeField]
        private int _idx;
        public int idx
        {
            get { return _idx;}
            set { _idx = value;}
        }
        [SerializeField]
        private string _name;
        public string name
        {
            get { return _name;}
            set { _name = value;}
        }
    }

    [System.Serializable]
    public class SampleTable : Table<SampleTableData, KeyValuePair<int,int>>
    {
    }
// #if UNITY_EDITOR
//     [CustomPropertyDrawer(typeof(SampleTableData),true)]
//     public class TableDrawer : PropertyDrawer
//     {
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             EditorGUI.PropertyField(position,property,label);
//         }
//     }
// #endif
}

