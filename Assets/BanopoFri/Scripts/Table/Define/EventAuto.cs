using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class EventAutoData
    {
        [SerializeField]
		private int _level;
		public int level
		{
			get { return _level;}
			set { _level = value;}
		}
		[SerializeField]
		private List<int> _auto_manager_lv;
		public List<int> auto_manager_lv
		{
			get { return _auto_manager_lv;}
			set { _auto_manager_lv = value;}
		}
		[SerializeField]
		private List<int> _auto_manager_count;
		public List<int> auto_manager_count
		{
			get { return _auto_manager_count;}
			set { _auto_manager_count = value;}
		}

    }

    [System.Serializable]
    public class EventAuto : Table<EventAutoData, int>
    {
    }
}

