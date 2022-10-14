using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class SeasonalSpecialCatData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _event_idx;
		public int event_idx
		{
			get { return _event_idx;}
			set { _event_idx = value;}
		}
		[SerializeField]
		private int _max_level;
		public int max_level
		{
			get { return _max_level;}
			set { _max_level = value;}
		}
		[SerializeField]
		private int _skill_type;
		public int skill_type
		{
			get { return _skill_type;}
			set { _skill_type = value;}
		}
		[SerializeField]
		private int _skill_buff_idx;
		public int skill_buff_idx
		{
			get { return _skill_buff_idx;}
			set { _skill_buff_idx = value;}
		}
		[SerializeField]
		private int _skill_value;
		public int skill_value
		{
			get { return _skill_value;}
			set { _skill_value = value;}
		}
		[SerializeField]
		private int _buff_incre_value;
		public int buff_incre_value
		{
			get { return _buff_incre_value;}
			set { _buff_incre_value = value;}
		}
		[SerializeField]
		private string _cat_icon;
		public string cat_icon
		{
			get { return _cat_icon;}
			set { _cat_icon = value;}
		}
		[SerializeField]
		private string _skill_icon;
		public string skill_icon
		{
			get { return _skill_icon;}
			set { _skill_icon = value;}
		}
		[SerializeField]
		private string _skill_name;
		public string skill_name
		{
			get { return _skill_name;}
			set { _skill_name = value;}
		}

    }

    [System.Serializable]
    public class SeasonalSpecialCat : Table<SeasonalSpecialCatData, int>
    {
    }
}

