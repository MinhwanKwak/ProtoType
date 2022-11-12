using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using BanpoFri;

public class LocalizeTextOverlapRemover : EditorWindow
{
	private string context;
	private string result_context;

	public static void ShowUI()
	{
		LocalizeTextOverlapRemover window = (LocalizeTextOverlapRemover)EditorWindow.GetWindowWithRect(typeof(LocalizeTextOverlapRemover),
		 new Rect(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, 600f, 700f));

		window.Show();
	}

	void OnGUI()
	{
		var style = EditorStyles.boldLabel;
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 25;
		GUILayout.Label("Text Remover", style);
		context = EditorGUILayout.TextArea(context, GUILayout.MaxHeight(200f));
		if (GUILayout.Button("적용", GUILayout.Height(40)))
		{
			Remover();
		}
		result_context = EditorGUILayout.TextArea(result_context, GUILayout.MaxHeight(200f));
	}

	private void Remover()
	{
		List<char> charList = new List<char>(){'¢', '£', '¤', '¥', '€', '₩', '₨', '៛',
		'₵', '₡', '₫', '₲', '₦', '₭' ,'₱' , '₪', '₸', '৳', '$', '₽', '₹', '฿'};
		for(int i = 33; i < 123; ++i)
		{
			charList.Add(System.Convert.ToChar(i));
		}
		var contextArr = context.ToCharArray();
		foreach(var ch in contextArr)
		{
			if (!charList.Contains(ch))
				charList.Add(ch);
		}

		result_context = new string(charList.ToArray());
	}

	public static string GetLocalizeText()
	{
		List<char> charList = new List<char>(){'¢', '£', '¤', '¥', '€', '₩', '₨', '៛',
		'₵', '₡', '₫', '₲', '₦', '₭' ,'₱' , '₪', '₸', '৳', '$', '₽', '₹', '฿'};
		for(int i = 33; i < 123; ++i)
		{
			charList.Add(System.Convert.ToChar(i));
		}

		var table = AssetDatabase.LoadAssetAtPath<Localize>("Assets/BanpoFri/TableAsset/Localize.asset");
		if(table != null)
		{
			System.Action<char[]> CheckChar = (arr) =>{
				foreach(var ch in arr)
				{
					if (!charList.Contains(ch))
						charList.Add(ch);
				}
			};
			foreach(var data in table.DataList)
			{
				CheckChar(data.en.ToCharArray());
				CheckChar(data.ko.ToCharArray());
				CheckChar(data.ja.ToCharArray());
			}
		}

		return new string(charList.ToArray());
	}
}
