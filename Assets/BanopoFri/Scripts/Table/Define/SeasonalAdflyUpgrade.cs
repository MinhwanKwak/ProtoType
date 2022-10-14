using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class SeasonalAdflyUpgradeData
    {
        [SerializeField]
		private int _level;
		public int level
		{
			get { return _level;}
			set { _level = value;}
		}
		[SerializeField]
		private int _increment_buff_idx;
		public int increment_buff_idx
		{
			get { return _increment_buff_idx;}
			set { _increment_buff_idx = value;}
		}
		[SerializeField]
		private int _increment_buff_value;
		public int increment_buff_value
		{
			get { return _increment_buff_value;}
			set { _increment_buff_value = value;}
		}
		[SerializeField]
		private int _cost_idx;
		public int cost_idx
		{
			get { return _cost_idx;}
			set { _cost_idx = value;}
		}
		[SerializeField]
		private int _cost_value;
		public int cost_value
		{
			get { return _cost_value;}
			set { _cost_value = value;}
		}

    }

    [System.Serializable]
    public class SeasonalAdflyUpgrade : Table<SeasonalAdflyUpgradeData, int>
    {
    }
}

