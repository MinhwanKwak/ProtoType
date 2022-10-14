using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ExpeditionCalendarData
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
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _hero_in;
		public int hero_in
		{
			get { return _hero_in;}
			set { _hero_in = value;}
		}
		[SerializeField]
		private string _stage_open;
		public string stage_open
		{
			get { return _stage_open;}
			set { _stage_open = value;}
		}
		[SerializeField]
		private int _ticket_idx;
		public int ticket_idx
		{
			get { return _ticket_idx;}
			set { _ticket_idx = value;}
		}
		[SerializeField]
		private int _ticket_max_count;
		public int ticket_max_count
		{
			get { return _ticket_max_count;}
			set { _ticket_max_count = value;}
		}
		[SerializeField]
		private int _ticket_charge_time;
		public int ticket_charge_time
		{
			get { return _ticket_charge_time;}
			set { _ticket_charge_time = value;}
		}
		[SerializeField]
		private int _event_period;
		public int event_period
		{
			get { return _event_period;}
			set { _event_period = value;}
		}
		[SerializeField]
		private int _event_cooltime;
		public int event_cooltime
		{
			get { return _event_cooltime;}
			set { _event_cooltime = value;}
		}
		[SerializeField]
		private string _img;
		public string img
		{
			get { return _img;}
			set { _img = value;}
		}
		[SerializeField]
		private int _open;
		public int open
		{
			get { return _open;}
			set { _open = value;}
		}
		[SerializeField]
		private int _ticket_gem_price;
		public int ticket_gem_price
		{
			get { return _ticket_gem_price;}
			set { _ticket_gem_price = value;}
		}

    }

    [System.Serializable]
    public class ExpeditionCalendar : Table<ExpeditionCalendarData, int>
    {
    }
}

