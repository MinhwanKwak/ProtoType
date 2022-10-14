using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class StageSetData
    {
        [SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}
		[SerializeField]
		private int _use_place;
		public int use_place
		{
			get { return _use_place;}
			set { _use_place = value;}
		}
		[SerializeField]
		private int _stage_value_increment;
		public int stage_value_increment
		{
			get { return _stage_value_increment;}
			set { _stage_value_increment = value;}
		}
		[SerializeField]
		private int _stage_cost_increment;
		public int stage_cost_increment
		{
			get { return _stage_cost_increment;}
			set { _stage_cost_increment = value;}
		}
		[SerializeField]
		private int _quest_clear_count;
		public int quest_clear_count
		{
			get { return _quest_clear_count;}
			set { _quest_clear_count = value;}
		}
		[SerializeField]
		private string _stage_bg;
		public string stage_bg
		{
			get { return _stage_bg;}
			set { _stage_bg = value;}
		}
		[SerializeField]
		private int _quest_guide_type;
		public int quest_guide_type
		{
			get { return _quest_guide_type;}
			set { _quest_guide_type = value;}
		}

    }

    [System.Serializable]
    public class StageSet : Table<StageSetData, int>
    {
    }
}

