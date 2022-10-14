using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class SaveQuestRewardData
    {
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
		private int _base_gem_value;
		public int base_gem_value
		{
			get { return _base_gem_value;}
			set { _base_gem_value = value;}
		}
		[SerializeField]
		private int _sale_start_rate;
		public int sale_start_rate
		{
			get { return _sale_start_rate;}
			set { _sale_start_rate = value;}
		}
		[SerializeField]
		private int _sale_add_rate;
		public int sale_add_rate
		{
			get { return _sale_add_rate;}
			set { _sale_add_rate = value;}
		}
		[SerializeField]
		private int _sale_end_rate;
		public int sale_end_rate
		{
			get { return _sale_end_rate;}
			set { _sale_end_rate = value;}
		}

    }

    [System.Serializable]
    public class SaveQuestReward : Table<SaveQuestRewardData, KeyValuePair<int,int>>
    {
    }
}

