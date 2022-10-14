using System;
using System.IO;
using UnityEngine;
using UnityEditor;

public class MasterDataConvertorWindow : EditorWindow
{
    private string import_context = "데이터 스트링을 넣어주세요";
	private string export_context = "";

	public static void ShowUI()
	{
		MasterDataConvertorWindow window = (MasterDataConvertorWindow)EditorWindow.GetWindowWithRect(typeof(MasterDataConvertorWindow),
		 new Rect(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, 600f, 700f));

		window.Show();
	}

	void OnGUI()
	{
		var style = EditorStyles.boldLabel;
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 25;
		GUILayout.Label("Master Data Convertor", style);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Import Master Data String");
		import_context = EditorGUILayout.TextArea(import_context, GUILayout.MaxHeight(200f));
		if (GUILayout.Button("데이터 넣기", GUILayout.Height(40)))
		{
            var filePath = $"{Application.dataPath}/Master.dat";
			File.WriteAllBytes(filePath, Convert.FromBase64String(import_context));
            EditorUtility.DisplayDialog("굿", "성공", "확인", null);
		}
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(40f);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Export Master Data String");
		export_context = EditorGUILayout.TextArea(export_context, GUILayout.MaxHeight(200f));
        if (GUILayout.Button("데이터 뽑기", GUILayout.Height(40)))
		{
            var filePath = $"{Application.dataPath}/Master.dat";
            if(File.Exists(filePath))
            {
                var dataBinary = File.ReadAllBytes(filePath);
			    export_context = Convert.ToBase64String(dataBinary);
            }
		}
        EditorGUILayout.EndVertical();
	}
}
