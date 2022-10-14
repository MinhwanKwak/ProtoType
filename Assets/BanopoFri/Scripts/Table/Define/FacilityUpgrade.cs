using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class FacilityUpgradeData
    {
        [SerializeField]
		private int _level;
		public int level
		{
			get { return _level;}
			set { _level = value;}
		}
		[SerializeField]
		private int _enter_value_increment;
		public int enter_value_increment
		{
			get { return _enter_value_increment;}
			set { _enter_value_increment = value;}
		}
		[SerializeField]
		private int _enter_cost_increment;
		public int enter_cost_increment
		{
			get { return _enter_cost_increment;}
			set { _enter_cost_increment = value;}
		}
		[SerializeField]
		private int _enter_speed_decrement;
		public int enter_speed_decrement
		{
			get { return _enter_speed_decrement;}
			set { _enter_speed_decrement = value;}
		}
		[SerializeField]
		private int _manufacture_value_increment;
		public int manufacture_value_increment
		{
			get { return _manufacture_value_increment;}
			set { _manufacture_value_increment = value;}
		}
		[SerializeField]
		private int _manufacture_cost_increment;
		public int manufacture_cost_increment
		{
			get { return _manufacture_cost_increment;}
			set { _manufacture_cost_increment = value;}
		}
		[SerializeField]
		private int _manufacture_speed_decrement;
		public int manufacture_speed_decrement
		{
			get { return _manufacture_speed_decrement;}
			set { _manufacture_speed_decrement = value;}
		}
		[SerializeField]
		private int _exit_value_increment;
		public int exit_value_increment
		{
			get { return _exit_value_increment;}
			set { _exit_value_increment = value;}
		}
		[SerializeField]
		private int _exit_cost_increment;
		public int exit_cost_increment
		{
			get { return _exit_cost_increment;}
			set { _exit_cost_increment = value;}
		}
		[SerializeField]
		private int _exit_speed_decrement;
		public int exit_speed_decrement
		{
			get { return _exit_speed_decrement;}
			set { _exit_speed_decrement = value;}
		}
		[SerializeField]
		private int _value_grade_bonus;
		public int value_grade_bonus
		{
			get { return _value_grade_bonus;}
			set { _value_grade_bonus = value;}
		}
		[SerializeField]
		private int _upgrade_reward_button;
		public int upgrade_reward_button
		{
			get { return _upgrade_reward_button;}
			set { _upgrade_reward_button = value;}
		}
		[SerializeField]
		private int _field_grade;
		public int field_grade
		{
			get { return _field_grade;}
			set { _field_grade = value;}
		}

    }

    [System.Serializable]
    public class FacilityUpgrade : Table<FacilityUpgradeData, int>
    {
    }
}

