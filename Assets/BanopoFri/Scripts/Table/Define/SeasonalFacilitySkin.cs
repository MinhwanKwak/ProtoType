using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class SeasonalFacilitySkinData
    {
        [SerializeField]
		private int _event_idx;
		public int event_idx
		{
			get { return _event_idx;}
			set { _event_idx = value;}
		}
		[SerializeField]
		private int _skin_idx;
		public int skin_idx
		{
			get { return _skin_idx;}
			set { _skin_idx = value;}
		}
		[SerializeField]
		private int _skin_type;
		public int skin_type
		{
			get { return _skin_type;}
			set { _skin_type = value;}
		}
		[SerializeField]
		private int _facility_idx;
		public int facility_idx
		{
			get { return _facility_idx;}
			set { _facility_idx = value;}
		}
		[SerializeField]
		private int _increment_buff_idx;
		public int increment_buff_idx
		{
			get { return _increment_buff_idx;}
			set { _increment_buff_idx = value;}
		}
		[SerializeField]
		private int _cost_currency_idx;
		public int cost_currency_idx
		{
			get { return _cost_currency_idx;}
			set { _cost_currency_idx = value;}
		}
		[SerializeField]
		private int _cost_value;
		public int cost_value
		{
			get { return _cost_value;}
			set { _cost_value = value;}
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
    public class SeasonalFacilitySkin : Table<SeasonalFacilitySkinData, KeyValuePair<int,int>>
    {
    }
}

