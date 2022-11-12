using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BanpoFri
{
	public enum KeyTypeCategory
	{
		none,
		Single,
		Pair,
	}
	public enum KeyType
	{
		none,
		Int,
		String,
	}

	public class TableCreatorWindow : EditorWindow
	{
		private string table_name = string.Empty;
		private KeyType type1 = KeyType.none;
		private KeyType type2 = KeyType.none;
		private KeyTypeCategory typeCategory = KeyTypeCategory.none;
		private string keyColName1 = string.Empty;
		private string keyColName2 = string.Empty;
		private string csv_context = string.Empty;
		private System.Action reloadCallback = null;
		private bool saveScriptFile = false;
		private List<List<string>> cachedDataList = new List<List<string>>();
		private List<int> cachedSkipColIndex = new List<int>();

		private const string SampleDataName = "{@DataName}";
		private const string SampleDataValue = "{@DataValue}";
		private const string SampleClassName = "{@ClassName}";
		private const string SampleKeyType = "{@KeyType}";
		private const string ScriptSavePath = "BanpoFri/Scripts/Table/Define";
		private const string TableResourceSavePath = "Assets/BanpoFri/TableAsset";

		void OnEnable()
		{
			AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
			AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
		}

		void OnDisable()
		{
			AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
			AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
		}

		public void OnBeforeAssemblyReload()
		{
		}

		public void OnAfterAssemblyReload()
		{
			if (saveScriptFile)
			{
				saveScriptFile = false;
				CreateTableAsset();
			}
		}

		void OnGUI()
		{
			EditorGUILayout.Space();
			var style = EditorStyles.boldLabel;
			style.alignment = TextAnchor.MiddleCenter;
			style.fontSize = 25;
			GUILayout.Label("Table Creator", style);
			table_name = EditorGUILayout.TextField("Table Name : ", table_name);
			EditorGUILayout.BeginHorizontal();
			typeCategory = (KeyTypeCategory)EditorGUILayout.EnumPopup("SelectKeyType : ", typeCategory);
			if (typeCategory == KeyTypeCategory.Single)
			{
				type1 = (KeyType)EditorGUILayout.EnumPopup("Type1 : ", type1);
			}
			else if (typeCategory == KeyTypeCategory.Pair)
			{
				type1 = (KeyType)EditorGUILayout.EnumPopup("Type1 : ", type1);
				type2 = (KeyType)EditorGUILayout.EnumPopup("Type2 : ", type2);
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			if (typeCategory == KeyTypeCategory.Single)
			{
				keyColName1 = EditorGUILayout.TextField("Key Field Name : ", keyColName1);
				keyColName2 = string.Empty;
			}
			else if (typeCategory == KeyTypeCategory.Pair)
			{
				keyColName1 = EditorGUILayout.TextField("Key Field Name : ", keyColName1);
				keyColName2 = EditorGUILayout.TextField("Second Key Field Name : ", keyColName2);
			}
			else
			{
				keyColName1 = string.Empty;
				keyColName2 = string.Empty;
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal(
				GUILayout.MinHeight(200f),
				GUILayout.MinWidth(600f),
				GUILayout.MaxHeight(200f));
			EditorGUILayout.LabelField("CSV Context : ");
			csv_context = EditorGUILayout.TextArea(csv_context, GUILayout.MaxHeight(200f));
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("생성", GUILayout.Height(40)))
			{
				CreateTable();
			}

			if (GUILayout.Button("추가(생성 후 클릭)", GUILayout.Height(40)))
			{
				AddTable();
			}
			EditorGUILayout.EndHorizontal();
		}

		void CreateTable()
		{
			if (typeCategory == KeyTypeCategory.none || type1 == KeyType.none)
			{
				EditorUtility.DisplayDialog("Error", "타입 카테고리를 선택해주세요.", "확인", null);
				return;
			}
			if (string.IsNullOrEmpty(table_name))
			{
				EditorUtility.DisplayDialog("Error", "테이블 명을 입력하세요.", "확인", null);
				return;
			}
			if (typeCategory == KeyTypeCategory.Pair)
			{
				if (type2 == KeyType.none)
				{
					EditorUtility.DisplayDialog("Error", "타입 2 카테고리를 선택해주세요.", "확인", null);
					return;
				}
				if (string.IsNullOrEmpty(keyColName2))
				{
					EditorUtility.DisplayDialog("Error", "2번 Key Field Name을 입력해주세요.", "확인", null);
					return;
				}
			}
			if (string.IsNullOrEmpty(keyColName1))
			{
				EditorUtility.DisplayDialog("Error", "1번 Key Field Name을 입력해주세요.", "확인", null);
				return;
			}

			CreateDataList();
			var dicDefine = new List<KeyValuePair<string, string>>();

			Func<int, string> SearchType = (index) =>
			{
				var iterCnt = 0;
				foreach (var row in cachedDataList)
				{
					if (iterCnt == 0)
					{
						++iterCnt;
						continue;
					}
					var value = row[index];
					if (!string.IsNullOrEmpty(value))
					{
						int intValue = 0;
						if (int.TryParse(value, out intValue))
						{
							bool findBigInt = false;
							for (int i = 0; i < cachedDataList.Count; ++i)
							{
								if (i == 0)
									continue;

								if (cachedDataList[i][index].Length > 9)
								{
									findBigInt = true;
									break;
								}
							}

							if (findBigInt)
								return "System.Numerics.BigInteger";
							else
								return "int";
						}

						float floatValue = 0f;
						if (float.TryParse(value, out floatValue))
						{
							return "float";
						}

						if (value.Contains(";"))
						{
							var elementType = "string";
							var values = value.Split(';');

							if (int.TryParse(values[0], out intValue))
							{
								elementType = "int";
							}
							else if (float.TryParse(values[0], out floatValue))
							{
								elementType = "float";
							}

							return $"List<{elementType}>";
						}
					}
				}
				return "string";
			};

			var findIndex = 0;
			foreach (var v in cachedDataList[0])
			{
				if (v.Contains("#"))
				{
					cachedSkipColIndex.Add(findIndex);
					++findIndex;
					continue;
				}

				var typeString = SearchType(findIndex);
				dicDefine.Add(new KeyValuePair<string, string>(typeString, v));
				++findIndex;
			}
			CreateTableScript(dicDefine);
		}

		void CreateDataList()
		{
			csv_context = csv_context.Replace("\\n", "\n");
			var csv_rows = csv_context.Split(Environment.NewLine[0]);

			if (csv_rows.Length < 2)
			{
				EditorUtility.DisplayDialog("Error", "데이터가 없습니다.", "확인", null);
				return;
			}
			cachedDataList.Clear();

			foreach (var row in csv_rows)
			{
				var csv_cols = row.Split('\t');
				if (csv_cols.Length < 2)
				{
					EditorUtility.DisplayDialog("Error", "문법 오류가 발생했습니다.", "확인", null);
					return;
				}

				for (int i = 0; i < csv_cols.Length; ++i)
				{
					csv_cols[i] = csv_cols[i].Trim();
					if (csv_cols[i].Contains("\\enter"))
					{
						csv_cols[i] = csv_cols[i].Replace("\\enter", "\n");
					}

				}

				cachedDataList.Add(new List<string>(csv_cols));
			}
		}

		public static void ShowUI()
		{
			TableCreatorWindow window = (TableCreatorWindow)EditorWindow.GetWindowWithRect(typeof(TableCreatorWindow),
			 new Rect(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, 600f, 400f));

			window.Show();
		}

		void CreateTableScript(List<KeyValuePair<string, string>> dicDefines)
		{
			var scriptSample = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/BanpoFri/Editor/TableSample.txt", typeof(TextAsset));
			var scriptContext = scriptSample.text;

			var strDefines = string.Empty;
			var keyTypeString = string.Empty;

			var strValFormat =
		@"[SerializeField]
		private {0} _{1};
		public {0} {1}
		{{
			get {{ return _{1};}}
			set {{ _{1} = value;}}
		}}
";
			var bigIntFormat =
		@"[SerializeField]
		private byte[] _{0};
		public System.Numerics.BigInteger {0}
		{{
			get {{ return new System.Numerics.BigInteger(_{0});}}
			set {{ _{0} = value.ToByteArray();}}
		}}
";
			var defineValues = new List<string>();
			bool first = false;
			foreach (var iter in dicDefines)
			{
				if (first)
				{
					if (iter.Key.Contains("BigInteger"))
						strDefines += $"\t\t{string.Format(bigIntFormat, iter.Value)}";
					else
						strDefines += $"\t\t{string.Format(strValFormat, iter.Key, iter.Value)}";
				}
				else
				{
					first = true;
					if (iter.Key.Contains("BigInteger"))
						strDefines += string.Format(bigIntFormat, iter.Value);
					else
						strDefines += string.Format(strValFormat, iter.Key, iter.Value);
				}

				defineValues.Add(iter.Value);
			}

			if (typeCategory == KeyTypeCategory.Single)
			{
				keyTypeString = type1 == KeyType.Int ? "int" : "string";
			}
			else
			{
				var type1String = type1 == KeyType.Int ? "int" : "string";
				var type2String = type2 == KeyType.Int ? "int" : "string";
				keyTypeString = $"KeyValuePair<{type1String},{type2String}>";
			}

			scriptContext = scriptContext.Replace(SampleDataName, $"{table_name}Data");
			scriptContext = scriptContext.Replace(SampleDataValue, strDefines);
			scriptContext = scriptContext.Replace(SampleClassName, table_name);
			scriptContext = scriptContext.Replace(SampleKeyType, keyTypeString);
			try
			{
				SaveCShapScriptFile(scriptContext);
			}
			catch (System.Exception ex)
			{
				EditorUtility.DisplayDialog("Error", "문법 오류가 발생했습니다. 저장을 실패했습니다.", "확인", null);
			}

		}

		void SaveCShapScriptFile(string _context)
		{
			var fileFullPath = Path.Combine(Application.dataPath, ScriptSavePath);
			fileFullPath = Path.Combine(fileFullPath, $"{table_name}.cs");

			if (File.Exists(fileFullPath))
			{
				File.Delete(fileFullPath);
			}

			if (!File.Exists(fileFullPath))
			{
				using (var saveFile = File.CreateText(fileFullPath))
				{
					saveFile.Write(_context);
				}
				saveScriptFile = true;
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
			else
			{
				EditorUtility.DisplayDialog("Error", "대상 파일이 존재합니다.", "확인", null);
			}
		}

		void CreateTableAsset()
		{
			CreateDataList();
			cachedSkipColIndex.Clear();
			var findIndex = 0;
			foreach (var v in cachedDataList[0])
			{
				if (v.Contains("#"))
				{
					cachedSkipColIndex.Add(findIndex);
				}
				++findIndex;
			}

			var type = Type.GetType($"BanpoFri.{table_name},BanpoFri");
			var dataType = Type.GetType($"BanpoFri.{table_name}Data,BanpoFri");
			var asset = CreateInstance(type);

			var listKeySchem1 = type.GetProperty("FirstKey");
			var listKeySchem2 = type.GetProperty("SecondKey");

			var listKeyObj1 = listKeySchem1.GetValue(asset) as System.Collections.IList;
			var listKeyObj2 = listKeySchem2.GetValue(asset) as System.Collections.IList;
			var listSchem = type.GetProperty("DataList");
			var listObj = listSchem.GetValue(asset) as System.Collections.IList;
			listObj.Clear();
			listKeyObj1.Clear();
			listKeyObj2.Clear();

			for (int i = 1; i < cachedDataList.Count; ++i)
			{
				var dataEntity = Activator.CreateInstance(dataType);
				int valueIndex = 0;
				foreach (var prop in dataType.GetProperties())
				{
					while (cachedSkipColIndex.Contains(valueIndex))
					{
						++valueIndex;
					}

					if (string.IsNullOrEmpty(cachedDataList[i][valueIndex]))
					{
						++valueIndex;
						continue;
					}

					if (keyColName1 == prop.Name)
					{
						listKeyObj1.Add(cachedDataList[i][valueIndex]);
					}
					else if (keyColName2 == prop.Name)
					{
						listKeyObj2.Add(cachedDataList[i][valueIndex]);
					}
					if (prop.PropertyType == typeof(System.Numerics.BigInteger))
					{
						prop.SetValue(dataEntity, System.Numerics.BigInteger.Parse(cachedDataList[i][valueIndex]));
					}
					else if (prop.PropertyType == typeof(int))
					{
						prop.SetValue(dataEntity, int.Parse(cachedDataList[i][valueIndex]));
					}
					else if (prop.PropertyType == typeof(float))
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
					else if (prop.PropertyType == typeof(string))
					{
						prop.SetValue(dataEntity, cachedDataList[i][valueIndex]);
					}
					++valueIndex;
				}
				listObj.Add(dataEntity);
			}

			AssetDatabase.CreateAsset(asset, Path.Combine(TableResourceSavePath, $"{table_name}.asset"));
			AssetDatabase.Refresh();

			EditorUtility.DisplayDialog("성공", "생성 완료! 추가 버튼을 눌러주세요.", "확인", null);
		}
		void AddTable()
		{
			var inst = AssetDatabase.LoadAssetAtPath(Path.Combine(TableResourceSavePath, $"{table_name}.asset"), typeof(Table));
			if (inst != null)
			{
				var tableAsset = (Table)inst;
				if (tableAsset)
				{
					var tables = (Tables)AssetDatabase.LoadAssetAtPath(Path.Combine(TableResourceSavePath, "Tables.asset"), typeof(Tables));
					if (tables)
					{
						var type = Type.GetType($"BanpoFri.{table_name},BanpoFri");
						tables.AddTable(type, tableAsset);
						EditorUtility.SetDirty(tables);
						AssetDatabase.SaveAssets();
						AssetDatabase.Refresh();
					}
				}
			}
			else
			{
				EditorUtility.DisplayDialog("Error", "아직 파일이 존재하지 않습니다. 잠시 후 시도해주세요.", "확인", null);
			}
			EditorUtility.DisplayDialog("긋", "성공", "확인", null);
		}
	}
}
