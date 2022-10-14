using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class SeasonalMissionData
    {
        [SerializeField]
		private int _event_idx;
		public int event_idx
		{
			get { return _event_idx;}
			set { _event_idx = value;}
		}
		[SerializeField]
		private int _mission_order;
		public int mission_order
		{
			get { return _mission_order;}
			set { _mission_order = value;}
		}
		[SerializeField]
		private byte[] _mission_value;
		public System.Numerics.BigInteger mission_value
		{
			get { return new System.Numerics.BigInteger(_mission_value);}
			set { _mission_value = value.ToByteArray();}
		}
		[SerializeField]
		private List<int> _auto_lv;
		public List<int> auto_lv
		{
			get { return _auto_lv;}
			set { _auto_lv = value;}
		}
		[SerializeField]
		private List<int> _auto_count;
		public List<int> auto_count
		{
			get { return _auto_count;}
			set { _auto_count = value;}
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
		private int _p_reward_type_1;
		public int p_reward_type_1
		{
			get { return _p_reward_type_1;}
			set { _p_reward_type_1 = value;}
		}
		[SerializeField]
		private int _p_reward_idx_1;
		public int p_reward_idx_1
		{
			get { return _p_reward_idx_1;}
			set { _p_reward_idx_1 = value;}
		}
		[SerializeField]
		private int _p_reward_value_1;
		public int p_reward_value_1
		{
			get { return _p_reward_value_1;}
			set { _p_reward_value_1 = value;}
		}
		[SerializeField]
		private int _p_reward_type_2;
		public int p_reward_type_2
		{
			get { return _p_reward_type_2;}
			set { _p_reward_type_2 = value;}
		}
		[SerializeField]
		private int _p_reward_idx_2;
		public int p_reward_idx_2
		{
			get { return _p_reward_idx_2;}
			set { _p_reward_idx_2 = value;}
		}
		[SerializeField]
		private int _p_reward_value_2;
		public int p_reward_value_2
		{
			get { return _p_reward_value_2;}
			set { _p_reward_value_2 = value;}
		}

    }

    [System.Serializable]
    public class SeasonalMission : Table<SeasonalMissionData, KeyValuePair<int,int>>
    {
    }
}

