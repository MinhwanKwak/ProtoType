using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class MissionDayworkData
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
		private int _open_stage;
		public int open_stage
		{
			get { return _open_stage;}
			set { _open_stage = value;}
		}
		[SerializeField]
		private string _desc;
		public string desc
		{
			get { return _desc;}
			set { _desc = value;}
		}
		[SerializeField]
		private int _count;
		public int count
		{
			get { return _count;}
			set { _count = value;}
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
		private string _icon;
		public string icon
		{
			get { return _icon;}
			set { _icon = value;}
		}

    }

    [System.Serializable]
    public class MissionDaywork : Table<MissionDayworkData, int>
    {
    }
}

