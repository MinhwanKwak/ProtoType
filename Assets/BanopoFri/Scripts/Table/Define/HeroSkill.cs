using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class HeroSkillData
    {
        [SerializeField]
		private int _skill_idx;
		public int skill_idx
		{
			get { return _skill_idx;}
			set { _skill_idx = value;}
		}
		[SerializeField]
		private int _buff_idx;
		public int buff_idx
		{
			get { return _buff_idx;}
			set { _buff_idx = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private string _desc;
		public string desc
		{
			get { return _desc;}
			set { _desc = value;}
		}
		[SerializeField]
		private string _effect_text;
		public string effect_text
		{
			get { return _effect_text;}
			set { _effect_text = value;}
		}
		[SerializeField]
		private int _open_level;
		public int open_level
		{
			get { return _open_level;}
			set { _open_level = value;}
		}
		[SerializeField]
		private int _holding_time;
		public int holding_time
		{
			get { return _holding_time;}
			set { _holding_time = value;}
		}
		[SerializeField]
		private int _cooltime_base;
		public int cooltime_base
		{
			get { return _cooltime_base;}
			set { _cooltime_base = value;}
		}
		[SerializeField]
		private int _display_value;
		public int display_value
		{
			get { return _display_value;}
			set { _display_value = value;}
		}
		[SerializeField]
		private string _Skill_Icon;
		public string Skill_Icon
		{
			get { return _Skill_Icon;}
			set { _Skill_Icon = value;}
		}

    }

    [System.Serializable]
    public class HeroSkill : Table<HeroSkillData, int>
    {
    }
}

