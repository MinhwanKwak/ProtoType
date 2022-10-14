using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class EventGachaData
    {
        [SerializeField]
		private int _level;
		public int level
		{
			get { return _level;}
			set { _level = value;}
		}
		[SerializeField]
		private byte[] _value;
		public System.Numerics.BigInteger value
		{
			get { return new System.Numerics.BigInteger(_value);}
			set { _value = value.ToByteArray();}
		}
		[SerializeField]
		private byte[] _upgrade_cost;
		public System.Numerics.BigInteger upgrade_cost
		{
			get { return new System.Numerics.BigInteger(_upgrade_cost);}
			set { _upgrade_cost = value.ToByteArray();}
		}
		[SerializeField]
		private int _time;
		public int time
		{
			get { return _time;}
			set { _time = value;}
		}
		[SerializeField]
		private List<int> _auto_manager_lv;
		public List<int> auto_manager_lv
		{
			get { return _auto_manager_lv;}
			set { _auto_manager_lv = value;}
		}
		[SerializeField]
		private List<int> _auto_manager_count;
		public List<int> auto_manager_count
		{
			get { return _auto_manager_count;}
			set { _auto_manager_count = value;}
		}
		[SerializeField]
		private byte[] _gacha_cost;
		public System.Numerics.BigInteger gacha_cost
		{
			get { return new System.Numerics.BigInteger(_gacha_cost);}
			set { _gacha_cost = value.ToByteArray();}
		}
		[SerializeField]
		private int _gacha_cost_increment;
		public int gacha_cost_increment
		{
			get { return _gacha_cost_increment;}
			set { _gacha_cost_increment = value;}
		}
		[SerializeField]
		private int _random_idx;
		public int random_idx
		{
			get { return _random_idx;}
			set { _random_idx = value;}
		}
		[SerializeField]
		private int _random_idx_challange;
		public int random_idx_challange
		{
			get { return _random_idx_challange;}
			set { _random_idx_challange = value;}
		}

    }

    [System.Serializable]
    public class EventGacha : Table<EventGachaData, int>
    {
    }
}

