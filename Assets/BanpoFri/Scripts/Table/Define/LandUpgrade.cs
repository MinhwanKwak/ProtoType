using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class LandUpgradeData
    {
        [SerializeField]
		private int _grade;
		public int grade
		{
			get { return _grade;}
			set { _grade = value;}
		}
		[SerializeField]
		private int _profit;
		public int profit
		{
			get { return _profit;}
			set { _profit = value;}
		}
		[SerializeField]
		private int _cost;
		public int cost
		{
			get { return _cost;}
			set { _cost = value;}
		}
		[SerializeField]
		private int _event_group;
		public int event_group
		{
			get { return _event_group;}
			set { _event_group = value;}
		}

    }

    [System.Serializable]
    public class LandUpgrade : Table<LandUpgradeData, int>
    {
    }
}

