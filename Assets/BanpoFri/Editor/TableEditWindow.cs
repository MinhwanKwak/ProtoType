using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BanpoFri
{
    public class TableEditWindow : EditorWindow
    {
        public Table target = null;
        private string csv_context = string.Empty;
        private string keyColName1 = string.Empty;
		private string keyColName2 = string.Empty;
        private List<List<string>> cachedDataList = new List<List<string>>();
        private List<int> cachedSkipColIndex = new List<int>();

        void OnGUI()
		{
            EditorGUILayout.Space();
			var style = EditorStyles.boldLabel;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = 25;
			GUILayout.Label("Table Editor", style);
			if(GUILayout.Button("수정", GUILayout.Height(40)))
			{
				try{
                    Modify();
                }
                catch(System.Exception ex)
                {
                    EditorUtility.DisplayDialog("Error", "문법 혹은 선언 된 스크립트와 내용이 다릅니다. 구조가 바뀐경우 테이블 데이터를 삭제하고 테이블 생성 창에서 다시 생성해주세요.", "확인", null);
                }
			}
            keyColName1 = EditorGUILayout.TextField("Key Field Name : ", keyColName1);
            keyColName2 = EditorGUILayout.TextField("Second Key Field Name : ", keyColName2);
            EditorGUILayout.BeginHorizontal(
				GUILayout.MinHeight(200f),
				GUILayout.MinWidth(600f),
				GUILayout.MaxHeight(200f));
			EditorGUILayout.LabelField("CSV Context : ");
			csv_context = EditorGUILayout.TextArea(csv_context, GUILayout.MaxHeight(200f));
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			
        }

        void Modify()
        {
            if(string.IsNullOrEmpty(keyColName1))
            {
                EditorUtility.DisplayDialog("Error", "Key Field Name이 비었습니다.", "확인", null);
                return;
            }
            CreateDataList();
			cachedSkipColIndex.Clear();
			var findIndex = 0;
			foreach(var v in cachedDataList[0])
			{
				if(v.Contains("#"))
				{
					cachedSkipColIndex.Add(findIndex);
				}
				++findIndex;
			}

			var type = target.GetType();
            var dataType = type.GetProperty("GenericDataType").PropertyType;
			var asset = target;
			
            
			var listKeySchem1 = type.GetProperty("FirstKey");
			var listKeySchem2 = type.GetProperty("SecondKey");

			var listKeyObj1 = listKeySchem1.GetValue(asset) as System.Collections.IList;
			var listKeyObj2 = listKeySchem2.GetValue(asset) as System.Collections.IList;
			var listSchem = type.GetProperty("DataList");
			var listObj = listSchem.GetValue(asset) as System.Collections.IList;
			listObj.Clear();
			listKeyObj1.Clear();
			listKeyObj2.Clear();

			for(int i = 1; i < cachedDataList.Count; ++i)
			{
				var dataEntity = Activator.CreateInstance(dataType);
				int valueIndex = 0;
				foreach(var prop in dataType.GetProperties())
				{					
					while(cachedSkipColIndex.Contains(valueIndex))
					{
						++valueIndex;
					}

					if(string.IsNullOrEmpty(cachedDataList[i][valueIndex]))
					{
						++valueIndex;
						continue;
					}

					if(keyColName1 == prop.Name)
					{
						listKeyObj1.Add(cachedDataList[i][valueIndex]);
					}
					else if(keyColName2 == prop.Name)
					{
						listKeyObj2.Add(cachedDataList[i][valueIndex]);
					}
					if(prop.PropertyType == typeof(System.Numerics.BigInteger))
					{
						prop.SetValue(dataEntity, System.Numerics.BigInteger.Parse(cachedDataList[i][valueIndex]));
					}
					else if(prop.PropertyType == typeof(int))
					{
						prop.SetValue(dataEntity, int.Parse(cachedDataList[i][valueIndex]));
					}
					else if(prop.PropertyType == typeof(float))
					{
						prop.SetValue(dataEntity, float.Parse(cachedDataList[i][valueIndex]));
					}
					else if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
					{
						var entityList = Activator.CreateInstance(prop.PropertyType) as System.Collections.IList;
						var entityValues = cachedDataList[i][valueIndex].Split(';');
						foreach (var entity in entityValues)
						{
							int intValue = 0;
							float floatValue = 0;
							if (int.TryParse(entity, out intValue))
							{
								entityList.Add(intValue);
							}
							else if (float.TryParse(entity, out floatValue))
							{
								entityList.Add(floatValue);
							}
							else
								entityList.Add(entity);
						}
						prop.SetValue(dataEntity, entityList);
					}
					else if(prop.PropertyType == typeof(string))
					{	
						prop.SetValue(dataEntity, cachedDataList[i][valueIndex]);
					}	
					++valueIndex;
				}
				listObj.Add(dataEntity);
			}
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();	
			
			EditorUtility.DisplayDialog("성공", "수정 완료", "확인", null);
		}

        void CreateDataList()
		{
			csv_context = csv_context.Replace("\\n","\n");
			var csv_rows = csv_context.Split(Environment.NewLine[0]);

			if(csv_rows.Length < 2)
			{
				EditorUtility.DisplayDialog("Error", "데이터가 없습니다.", "확인", null);
				return;
			}
			cachedDataList.Clear();			

			foreach(var row in csv_rows){
				var csv_cols = row.Split('\t');
				if(csv_cols.Length < 2)
				{
					EditorUtility.DisplayDialog("Error", "문법 오류가 발생했습니다.", "확인", null);
					return;
				}
				
				for(int i = 0; i< csv_cols.Length; ++i)
				{
					csv_cols[i] = csv_cols[i].Trim();
					if(csv_cols[i].Contains("\\enter"))
					{
						csv_cols[i] = csv_cols[i].Replace("\\enter", "\n");
					}
				}

				cachedDataList.Add(new List<string>(csv_cols));
			}
		}
        
        public static void ShowUI(Table _target)
		{            
			TableEditWindow window = (TableEditWindow)EditorWindow.GetWindowWithRect(typeof(TableEditWindow),
			 new Rect( Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, 600f, 400f));
			
            window.target = _target;
			window.Show();
		}
    }
}