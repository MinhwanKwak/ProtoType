using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class LandConditionData
    {
        [SerializeField]
		private int _condition;
		public int condition
		{
			get { return _condition;}
			set { _condition = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private List<int> _need_point;
		public List<int> need_point
		{
			get { return _need_point;}
			set { _need_point = value;}
		}
		[SerializeField]
		private int _bonus_profit;
		public int bonus_profit
		{
			get { return _bonus_profit;}
			set { _bonus_profit = value;}
		}
		[SerializeField]
		private string _bonus_profit_desc;
		public string bonus_profit_desc
		{
			get { return _bonus_profit_desc;}
			set { _bonus_profit_desc = value;}
		}
		[SerializeField]
		private string _icon;
		public string icon
		{
			get { return _icon;}
			set { _icon = value;}
		}

    }

    [System.Serializable]
    public class LandCondition : Table<LandConditionData, int>
    {
    }
}

