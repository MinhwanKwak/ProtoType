using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class SeasonalRouletteData
    {
        [SerializeField]
		private int _type;
		public int type
		{
			get { return _type;}
			set { _type = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private int _reward_type;
		public int reward_type
		{
			get { return _reward_type;}
			set { _reward_type = value;}
		}
		[SerializeField]
		private int _reward_idx;
		public int reward_idx
		{
			get { return _reward_idx;}
			set { _reward_idx = value;}
		}
		[SerializeField]
		private int _reward_value;
		public int reward_value
		{
			get { return _reward_value;}
			set { _reward_value = value;}
		}
		[SerializeField]
		private int _ratio;
		public int ratio
		{
			get { return _ratio;}
			set { _ratio = value;}
		}

    }

    [System.Serializable]
    public class SeasonalRoulette : Table<SeasonalRouletteData, KeyValuePair<int,int>>
    {
    }
}

