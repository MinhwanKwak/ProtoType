using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class MissionAchievementData
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
		private string _desc;
		public string desc
		{
			get { return _desc;}
			set { _desc = value;}
		}
		[SerializeField]
		private List<int> _count;
		public List<int> count
		{
			get { return _count;}
			set { _count = value;}
		}
		[SerializeField]
		private int _count_add;
		public int count_add
		{
			get { return _count_add;}
			set { _count_add = value;}
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
		private int _view;
		public int view
		{
			get { return _view;}
			set { _view = value;}
		}
		[SerializeField]
		private string _icon;
		public string icon
		{
			get { return _icon;}
			set { _icon = value;}
		}

    }

    [System.Serializable]
    public class MissionAchievement : Table<MissionAchievementData, int>
    {
    }
}

