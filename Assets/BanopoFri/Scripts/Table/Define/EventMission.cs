using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class EventMissionData
    {
        [SerializeField]
		private int _stage_idx;
		public int stage_idx
		{
			get { return _stage_idx;}
			set { _stage_idx = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private byte[] _value;
		public System.Numerics.BigInteger value
		{
			get { return new System.Numerics.BigInteger(_value);}
			set { _value = value.ToByteArray();}
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
		private int _premium_reward_type;
		public int premium_reward_type
		{
			get { return _premium_reward_type;}
			set { _premium_reward_type = value;}
		}
		[SerializeField]
		private int _premium_reward_idx;
		public int premium_reward_idx
		{
			get { return _premium_reward_idx;}
			set { _premium_reward_idx = value;}
		}
		[SerializeField]
		private int _premium_reward_value;
		public int premium_reward_value
		{
			get { return _premium_reward_value;}
			set { _premium_reward_value = value;}
		}

    }

    [System.Serializable]
    public class EventMission : Table<EventMissionData, KeyValuePair<int,int>>
    {
    }
}

