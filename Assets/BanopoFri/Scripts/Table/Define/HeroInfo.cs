using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class HeroInfoData
    {
        [SerializeField]
		private int _hero_idx;
		public int hero_idx
		{
			get { return _hero_idx;}
			set { _hero_idx = value;}
		}
		[SerializeField]
		private int _elite_type;
		public int elite_type
		{
			get { return _elite_type;}
			set { _elite_type = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _property_idx;
		public int property_idx
		{
			get { return _property_idx;}
			set { _property_idx = value;}
		}
		[SerializeField]
		private string _hero_icon;
		public string hero_icon
		{
			get { return _hero_icon;}
			set { _hero_icon = value;}
		}
		[SerializeField]
		private int _skill_idx;
		public int skill_idx
		{
			get { return _skill_idx;}
			set { _skill_idx = value;}
		}
		[SerializeField]
		private int _hero_open;
		public int hero_open
		{
			get { return _hero_open;}
			set { _hero_open = value;}
		}
		[SerializeField]
		private List<int> _grade_level;
		public List<int> grade_level
		{
			get { return _grade_level;}
			set { _grade_level = value;}
		}
		[SerializeField]
		private List<string> _grade_first_name;
		public List<string> grade_first_name
		{
			get { return _grade_first_name;}
			set { _grade_first_name = value;}
		}
		[SerializeField]
		private List<string> _grade_prefeb;
		public List<string> grade_prefeb
		{
			get { return _grade_prefeb;}
			set { _grade_prefeb = value;}
		}
		[SerializeField]
		private string _base_prefab;
		public string base_prefab
		{
			get { return _base_prefab;}
			set { _base_prefab = value;}
		}

    }

    [System.Serializable]
    public class HeroInfo : Table<HeroInfoData, int>
    {
    }
}

