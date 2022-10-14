using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ManagerUpgradeData
    {
        [SerializeField]
		private int _grade;
		public int grade
		{
			get { return _grade;}
			set { _grade = value;}
		}
		[SerializeField]
		private int _level;
		public int level
		{
			get { return _level;}
			set { _level = value;}
		}
		[SerializeField]
		private int _bonus_value;
		public int bonus_value
		{
			get { return _bonus_value;}
			set { _bonus_value = value;}
		}
		[SerializeField]
		private int _upgrade_cost;
		public int upgrade_cost
		{
			get { return _upgrade_cost;}
			set { _upgrade_cost = value;}
		}
		[SerializeField]
		private int _need_card_count;
		public int need_card_count
		{
			get { return _need_card_count;}
			set { _need_card_count = value;}
		}

    }

    [System.Serializable]
    public class ManagerUpgrade : Table<ManagerUpgradeData, KeyValuePair<int,int>>
    {
    }
}

