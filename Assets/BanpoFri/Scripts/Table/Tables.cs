using UnityEngine;
using System;
using System.Collections.Generic;

namespace BanpoFri
{
	public interface IMultiKeyTable {}
	public interface ILoader
	{
		void Load();
	}

	public class Tables : SingletonScriptableObject<Tables>, ILoader
	{
		[SerializeField]
		List<Table> tableList  = new List<Table>();

		private Dictionary<Type, Table> tableDic = new Dictionary<Type, Table>();

		public T GetTable<T>() where T : Table
		{
			var findType = typeof(T);
			if(tableDic.ContainsKey(findType))
				return tableDic[findType] as T;

			return null;
		}

		public void AddTable(System.Type tableType, Table table)
		{
			tableList.RemoveAll(x => x == null || x.GetType() == tableType);
			tableList.Add(table);
		}

        public void Load()
        {
			tableDic.Clear();
			foreach(var table in tableList)
			{
				tableDic.Add(table.GetType(), table);
				table.Load();
			}
        }
	}
}