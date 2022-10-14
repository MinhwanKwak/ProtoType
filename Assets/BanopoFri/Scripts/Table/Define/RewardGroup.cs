using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class RewardGroupData
    {
        [SerializeField]
		private int _group;
		public int group
		{
			get { return _group;}
			set { _group = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private int _reward_group;
		public int reward_group
		{
			get { return _reward_group;}
			set { _reward_group = value;}
		}
		[SerializeField]
		private int _type_idx;
		public int type_idx
		{
			get { return _type_idx;}
			set { _type_idx = value;}
		}
		[SerializeField]
		private int _count_min;
		public int count_min
		{
			get { return _count_min;}
			set { _count_min = value;}
		}
		[SerializeField]
		private int _count_max;
		public int count_max
		{
			get { return _count_max;}
			set { _count_max = value;}
		}
		[SerializeField]
		private string _resource_prefab;
		public string resource_prefab
		{
			get { return _resource_prefab;}
			set { _resource_prefab = value;}
		}
		[SerializeField]
		private string _resource_icon;
		public string resource_icon
		{
			get { return _resource_icon;}
			set { _resource_icon = value;}
		}

    }

    [System.Serializable]
    public class RewardGroup : Table<RewardGroupData, int>
    {
    }
}

