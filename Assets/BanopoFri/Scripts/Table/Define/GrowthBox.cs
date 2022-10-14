using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class GrowthBoxData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _grade;
		public int grade
		{
			get { return _grade;}
			set { _grade = value;}
		}
		[SerializeField]
		private int _need_point;
		public int need_point
		{
			get { return _need_point;}
			set { _need_point = value;}
		}
		[SerializeField]
		private int _reward_idx;
		public int reward_idx
		{
			get { return _reward_idx;}
			set { _reward_idx = value;}
		}
		[SerializeField]
		private string _reward_name;
		public string reward_name
		{
			get { return _reward_name;}
			set { _reward_name = value;}
		}

    }

    [System.Serializable]
    public class GrowthBox : Table<GrowthBoxData, int>
    {
    }
}

