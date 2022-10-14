using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class CompetitionData
    {
        [SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}
		[SerializeField]
		private int _stage_open_condition;
		public int stage_open_condition
		{
			get { return _stage_open_condition;}
			set { _stage_open_condition = value;}
		}
		[SerializeField]
		private int _stage_type;
		public int stage_type
		{
			get { return _stage_type;}
			set { _stage_type = value;}
		}
		[SerializeField]
		private int _need_heart;
		public int need_heart
		{
			get { return _need_heart;}
			set { _need_heart = value;}
		}
		[SerializeField]
		private List<int> _need_properties_type;
		public List<int> need_properties_type
		{
			get { return _need_properties_type;}
			set { _need_properties_type = value;}
		}
		[SerializeField]
		private List<int> _need_properties_count;
		public List<int> need_properties_count
		{
			get { return _need_properties_count;}
			set { _need_properties_count = value;}
		}
		[SerializeField]
		private int _limit_properties_type;
		public int limit_properties_type
		{
			get { return _limit_properties_type;}
			set { _limit_properties_type = value;}
		}
		[SerializeField]
		private int _need_grade_type;
		public int need_grade_type
		{
			get { return _need_grade_type;}
			set { _need_grade_type = value;}
		}
		[SerializeField]
		private int _need_grade_count;
		public int need_grade_count
		{
			get { return _need_grade_count;}
			set { _need_grade_count = value;}
		}
		[SerializeField]
		private int _max_point;
		public int max_point
		{
			get { return _max_point;}
			set { _max_point = value;}
		}
		[SerializeField]
		private int _reward_type1;
		public int reward_type1
		{
			get { return _reward_type1;}
			set { _reward_type1 = value;}
		}
		[SerializeField]
		private int _reward_idx1;
		public int reward_idx1
		{
			get { return _reward_idx1;}
			set { _reward_idx1 = value;}
		}
		[SerializeField]
		private List<int> _reward_value1;
		public List<int> reward_value1
		{
			get { return _reward_value1;}
			set { _reward_value1 = value;}
		}
		[SerializeField]
		private int _reward_type2;
		public int reward_type2
		{
			get { return _reward_type2;}
			set { _reward_type2 = value;}
		}
		[SerializeField]
		private int _reward_idx2;
		public int reward_idx2
		{
			get { return _reward_idx2;}
			set { _reward_idx2 = value;}
		}
		[SerializeField]
		private int _reward_value2_min;
		public int reward_value2_min
		{
			get { return _reward_value2_min;}
			set { _reward_value2_min = value;}
		}
		[SerializeField]
		private int _reward_value2_max;
		public int reward_value2_max
		{
			get { return _reward_value2_max;}
			set { _reward_value2_max = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}

    }

    [System.Serializable]
    public class Competition : Table<CompetitionData, int>
    {
    }
}

