using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class DragonHuntData
    {
        [SerializeField]
		private int _hp_grade;
		public int hp_grade
		{
			get { return _hp_grade;}
			set { _hp_grade = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private string _dragon_hp_color;
		public string dragon_hp_color
		{
			get { return _dragon_hp_color;}
			set { _dragon_hp_color = value;}
		}
		[SerializeField]
		private int _dragon_hp;
		public int dragon_hp
		{
			get { return _dragon_hp;}
			set { _dragon_hp = value;}
		}
		[SerializeField]
		private int _dragon_atk;
		public int dragon_atk
		{
			get { return _dragon_atk;}
			set { _dragon_atk = value;}
		}
		[SerializeField]
		private int _dragon_atk_cooltime;
		public int dragon_atk_cooltime
		{
			get { return _dragon_atk_cooltime;}
			set { _dragon_atk_cooltime = value;}
		}
		[SerializeField]
		private int _fairy_cooltime;
		public int fairy_cooltime
		{
			get { return _fairy_cooltime;}
			set { _fairy_cooltime = value;}
		}
		[SerializeField]
		private int _fairy_speed;
		public int fairy_speed
		{
			get { return _fairy_speed;}
			set { _fairy_speed = value;}
		}
		[SerializeField]
		private List<int> _hero_merge_value;
		public List<int> hero_merge_value
		{
			get { return _hero_merge_value;}
			set { _hero_merge_value = value;}
		}
		[SerializeField]
		private List<int> _reward_currency_idx;
		public List<int> reward_currency_idx
		{
			get { return _reward_currency_idx;}
			set { _reward_currency_idx = value;}
		}
		[SerializeField]
		private List<int> _reward_currency_value;
		public List<int> reward_currency_value
		{
			get { return _reward_currency_value;}
			set { _reward_currency_value = value;}
		}
		[SerializeField]
		private List<int> _reward_currency_hp_rate;
		public List<int> reward_currency_hp_rate
		{
			get { return _reward_currency_hp_rate;}
			set { _reward_currency_hp_rate = value;}
		}
		[SerializeField]
		private int _reward_type;
		public int reward_type
		{
			get { return _reward_type;}
			set { _reward_type = value;}
		}
		[SerializeField]
		private int _reward_idx;
		public int reward_idx
		{
			get { return _reward_idx;}
			set { _reward_idx = value;}
		}
		[SerializeField]
		private int _reward_value;
		public int reward_value
		{
			get { return _reward_value;}
			set { _reward_value = value;}
		}

    }

    [System.Serializable]
    public class DragonHunt : Table<DragonHuntData, int>
    {
    }
}

