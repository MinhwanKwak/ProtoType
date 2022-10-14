using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class PentHouseUpgradeData
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
		private int _level_point_increment;
		public int level_point_increment
		{
			get { return _level_point_increment;}
			set { _level_point_increment = value;}
		}
		[SerializeField]
		private int _need_card_count;
		public int need_card_count
		{
			get { return _need_card_count;}
			set { _need_card_count = value;}
		}
		[SerializeField]
		private int _need_ticket_count;
		public int need_ticket_count
		{
			get { return _need_ticket_count;}
			set { _need_ticket_count = value;}
		}

    }

    [System.Serializable]
    public class PentHouseUpgrade : Table<PentHouseUpgradeData, KeyValuePair<int,int>>
    {
    }
}

