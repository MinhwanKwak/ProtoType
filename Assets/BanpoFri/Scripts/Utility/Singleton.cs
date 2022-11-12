using UnityEngine;

namespace BanpoFri
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		protected static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					GameObject obj;
					obj = GameObject.Find(typeof(T).Name);
					if (obj == null)
					{
						obj = new GameObject(typeof(T).Name);
						instance = obj.AddComponent<T>();
					}
					else
					{
						instance = obj.GetComponent<T>();
					}
				}
				return instance;
			}
		}

		public void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		public void Destroy()
		{
			Destroy(instance);
			instance = null;
		}
	}
}