using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ManagerExchangeData
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
		private string _desc;
		public string desc
		{
			get { return _desc;}
			set { _desc = value;}
		}
		[SerializeField]
		private int _exchange_type;
		public int exchange_type
		{
			get { return _exchange_type;}
			set { _exchange_type = value;}
		}
		[SerializeField]
		private int _grade;
		public int grade
		{
			get { return _grade;}
			set { _grade = value;}
		}
		[SerializeField]
		private int _card_type;
		public int card_type
		{
			get { return _card_type;}
			set { _card_type = value;}
		}
		[SerializeField]
		private int _value;
		public int value
		{
			get { return _value;}
			set { _value = value;}
		}
		[SerializeField]
		private int _need_point;
		public int need_point
		{
			get { return _need_point;}
			set { _need_point = value;}
		}
		[SerializeField]
		private int _time;
		public int time
		{
			get { return _time;}
			set { _time = value;}
		}
		[SerializeField]
		private string _icon;
		public string icon
		{
			get { return _icon;}
			set { _icon = value;}
		}
		[SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}

    }

    [System.Serializable]
    public class ManagerExchange : Table<ManagerExchangeData, int>
    {
    }
}

