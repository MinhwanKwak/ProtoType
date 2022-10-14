using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class PentHouseSetData
    {
        [SerializeField]
		private int _penthouse_set_idx;
		public int penthouse_set_idx
		{
			get { return _penthouse_set_idx;}
			set { _penthouse_set_idx = value;}
		}
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
		private int _shop_open_stage;
		public int shop_open_stage
		{
			get { return _shop_open_stage;}
			set { _shop_open_stage = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _parts_buff_idx;
		public int parts_buff_idx
		{
			get { return _parts_buff_idx;}
			set { _parts_buff_idx = value;}
		}
		[SerializeField]
		private int _collect_reward_type;
		public int collect_reward_type
		{
			get { return _collect_reward_type;}
			set { _collect_reward_type = value;}
		}
		[SerializeField]
		private int _collect_reward_idx;
		public int collect_reward_idx
		{
			get { return _collect_reward_idx;}
			set { _collect_reward_idx = value;}
		}
		[SerializeField]
		private int _collect_reward_value;
		public int collect_reward_value
		{
			get { return _collect_reward_value;}
			set { _collect_reward_value = value;}
		}
		[SerializeField]
		private int _set_point_increment;
		public int set_point_increment
		{
			get { return _set_point_increment;}
			set { _set_point_increment = value;}
		}
		[SerializeField]
		private string _resource_preview;
		public string resource_preview
		{
			get { return _resource_preview;}
			set { _resource_preview = value;}
		}
		[SerializeField]
		private List<int> _shortcut_type;
		public List<int> shortcut_type
		{
			get { return _shortcut_type;}
			set { _shortcut_type = value;}
		}

    }

    [System.Serializable]
    public class PentHouseSet : Table<PentHouseSetData, int>
    {
    }
}

