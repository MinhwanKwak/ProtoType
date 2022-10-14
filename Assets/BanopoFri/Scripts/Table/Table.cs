using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace BanpoFri
{
	[System.Serializable]
	public abstract class Table : ScriptableObject
	{
		public virtual void Load(){}
	}

	[System.Serializable]
	public class Table<T_Data, T_KEY> : Table 
		where T_Data : class 
	{
		public T_Data GenericDataType {get;set;}
		
		/*
		KeyValuePair의 경우 제네릭클래스여서 유니티에서 시리얼라이즈를 제공하지 않는다. 때문에 일차원적으로 분류
		*/
		[SerializeField]
		private List<string> mFirstKey = new List<string>();
		public List<string> FirstKey 
		{
			get{ return mFirstKey; }
			set{ mFirstKey = value;}
		}
		[SerializeField]
		private List<string> mSecondKey = new List<string>();
		public List<string> SecondKey 
		{
			get{ return mSecondKey; }
			set{ mSecondKey = value;}
		}
		[SerializeField]
		private List<T_Data> mDataList = new List<T_Data>();
		public List<T_Data> DataList 
		{
			get{ return mDataList; }
			set{ mDataList = value;}
		}

		[SerializeField]
		private Dictionary<T_KEY, T_Data> dicData = new Dictionary<T_KEY, T_Data>();

		public IEnumerable<T_KEY> GetKeys { get{ return dicData.Keys; } }

		public T_Data GetData(T_KEY key)
		{
			if(dicData.ContainsKey(key))
			{
				return dicData[key];
			}			
			return null;
		}

		public override void Load()
		{
			dicData.Clear();
			System.Func<System.Type, string, object> CheckValue = (type, obj) =>
			{
				object retVal = null;
				if(type == typeof(int))
				{
					retVal = int.Parse(obj);
				}
				else if(type == typeof(float))
				{
					retVal = float.Parse(obj);
				}
				else if(type == typeof(string))
				{
					retVal = obj;
				}
				return retVal;
			};
			
			if(typeof(T_KEY).IsGenericType)
			{
				var listKeyElementType = typeof(T_KEY).GetGenericArguments()[0];
				var listKeyElementType2 = typeof(T_KEY).GetGenericArguments()[1];
				
				var index = 0;
				foreach(var key in FirstKey)
				{
					var inst = (T_KEY)System.Activator.CreateInstance(
						typeof(T_KEY),
					 	CheckValue(listKeyElementType, key),
						CheckValue(listKeyElementType2, SecondKey[index]));
					dicData.Add(inst, mDataList[index]);
					++index;
				}
			}
			else
			{
				var index = 0;
				foreach(var key in FirstKey)
				{
					var keyValue  = (T_KEY) System.Convert.ChangeType(key, typeof(T_KEY));
					if(!dicData.ContainsKey(keyValue))
						dicData.Add(keyValue , mDataList[index]);
					++index;
				}
			}
		}
	}

	public class SingletonScriptableObject<T> : ScriptableObject
		where T : ScriptableObject, ILoader
	{
		public static bool IsCreate {get {return s_Instance != null;}}
		private static T s_Instance = null;

		public static T Instance
		{
			get
			{
				if (!s_Instance)
				{
					Create();
				}

				return s_Instance;
			}
		}

		public static IEnumerator Create()
		{
			var tableName = $"{typeof(T).Name}";
			var handler = Addressables.LoadAssetAsync<T>(tableName);
			while(!handler.IsDone)
				yield return null;

			s_Instance = handler.Result;
			s_Instance.Load();
		}
	}
}
