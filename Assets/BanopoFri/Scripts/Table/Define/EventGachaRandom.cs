using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class EventGachaRandomData
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
		private byte[] _reward_value;
		public System.Numerics.BigInteger reward_value
		{
			get { return new System.Numerics.BigInteger(_reward_value);}
			set { _reward_value = value.ToByteArray();}
		}
		[SerializeField]
		private int _ratio;
		public int ratio
		{
			get { return _ratio;}
			set { _ratio = value;}
		}
		[SerializeField]
		private int _jackpot_grade;
		public int jackpot_grade
		{
			get { return _jackpot_grade;}
			set { _jackpot_grade = value;}
		}

    }

    [System.Serializable]
    public class EventGachaRandom : Table<EventGachaRandomData, KeyValuePair<int,int>>
    {
    }
}

