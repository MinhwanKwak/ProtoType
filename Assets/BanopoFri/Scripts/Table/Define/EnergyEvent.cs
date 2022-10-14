using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class EnergyEventData
    {
        [SerializeField]
		private int _energy_idx;
		public int energy_idx
		{
			get { return _energy_idx;}
			set { _energy_idx = value;}
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
		private int _reward_value;
		public int reward_value
		{
			get { return _reward_value;}
			set { _reward_value = value;}
		}
		[SerializeField]
		private int _energy_value;
		public int energy_value
		{
			get { return _energy_value;}
			set { _energy_value = value;}
		}
		[SerializeField]
		private string _title_string;
		public string title_string
		{
			get { return _title_string;}
			set { _title_string = value;}
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
    public class EnergyEvent : Table<EnergyEventData, KeyValuePair<int,int>>
    {
    }
}

