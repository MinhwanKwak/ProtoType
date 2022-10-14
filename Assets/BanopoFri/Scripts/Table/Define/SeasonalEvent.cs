using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class SeasonalEventData
    {
        [SerializeField]
		private int _event_idx;
		public int event_idx
		{
			get { return _event_idx;}
			set { _event_idx = value;}
		}
		[SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _shop_penthouse_type;
		public int shop_penthouse_type
		{
			get { return _shop_penthouse_type;}
			set { _shop_penthouse_type = value;}
		}
		[SerializeField]
		private string _concept_icon;
		public string concept_icon
		{
			get { return _concept_icon;}
			set { _concept_icon = value;}
		}

    }

    [System.Serializable]
    public class SeasonalEvent : Table<SeasonalEventData, int>
    {
    }
}

