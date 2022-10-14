using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class RandomDragonData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private int _dagon_cave_idx;
		public int dagon_cave_idx
		{
			get { return _dagon_cave_idx;}
			set { _dagon_cave_idx = value;}
		}
		[SerializeField]
		private int _ratio;
		public int ratio
		{
			get { return _ratio;}
			set { _ratio = value;}
		}
		[SerializeField]
		private int _overlap_reward_idx;
		public int overlap_reward_idx
		{
			get { return _overlap_reward_idx;}
			set { _overlap_reward_idx = value;}
		}
		[SerializeField]
		private int _overlap_reward_value;
		public int overlap_reward_value
		{
			get { return _overlap_reward_value;}
			set { _overlap_reward_value = value;}
		}

    }

    [System.Serializable]
    public class RandomDragon : Table<RandomDragonData, KeyValuePair<int,int>>
    {
    }
}

