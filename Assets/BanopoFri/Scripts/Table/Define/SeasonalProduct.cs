using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class SeasonalProductData
    {
        [SerializeField]
		private int _event_idx;
		public int event_idx
		{
			get { return _event_idx;}
			set { _event_idx = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}
		[SerializeField]
		private int _costume_idx;
		public int costume_idx
		{
			get { return _costume_idx;}
			set { _costume_idx = value;}
		}

    }

    [System.Serializable]
    public class SeasonalProduct : Table<SeasonalProductData, KeyValuePair<int,int>>
    {
    }
}

