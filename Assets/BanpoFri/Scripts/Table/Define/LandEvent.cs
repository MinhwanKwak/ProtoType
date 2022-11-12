using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class LandEventData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _group;
		public int group
		{
			get { return _group;}
			set { _group = value;}
		}
		[SerializeField]
		private string _desc;
		public string desc
		{
			get { return _desc;}
			set { _desc = value;}
		}
		[SerializeField]
		private List<int> _point_variance;
		public List<int> point_variance
		{
			get { return _point_variance;}
			set { _point_variance = value;}
		}
		[SerializeField]
		private int _need_item;
		public int need_item
		{
			get { return _need_item;}
			set { _need_item = value;}
		}

    }

    [System.Serializable]
    public class LandEvent : Table<LandEventData, int>
    {
    }
}

