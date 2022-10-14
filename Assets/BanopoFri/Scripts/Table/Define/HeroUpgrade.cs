using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class HeroUpgradeData
    {
        [SerializeField]
		private int _level;
		public int level
		{
			get { return _level;}
			set { _level = value;}
		}
		[SerializeField]
		private int _need_exp;
		public int need_exp
		{
			get { return _need_exp;}
			set { _need_exp = value;}
		}
		[SerializeField]
		private int _elite_need_exp;
		public int elite_need_exp
		{
			get { return _elite_need_exp;}
			set { _elite_need_exp = value;}
		}
		[SerializeField]
		private int _power_value;
		public int power_value
		{
			get { return _power_value;}
			set { _power_value = value;}
		}
		[SerializeField]
		private int _property_rate;
		public int property_rate
		{
			get { return _property_rate;}
			set { _property_rate = value;}
		}
		[SerializeField]
		private int _hp_value;
		public int hp_value
		{
			get { return _hp_value;}
			set { _hp_value = value;}
		}
		[SerializeField]
		private int _skill_cooltime;
		public int skill_cooltime
		{
			get { return _skill_cooltime;}
			set { _skill_cooltime = value;}
		}

    }

    [System.Serializable]
    public class HeroUpgrade : Table<HeroUpgradeData, int>
    {
    }
}

