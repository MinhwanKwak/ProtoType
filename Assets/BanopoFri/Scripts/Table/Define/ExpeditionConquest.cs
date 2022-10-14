using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ExpeditionConquestData
    {
        [SerializeField]
		private int _chapter;
		public int chapter
		{
			get { return _chapter;}
			set { _chapter = value;}
		}
		[SerializeField]
		private string _chapter_name;
		public string chapter_name
		{
			get { return _chapter_name;}
			set { _chapter_name = value;}
		}
		[SerializeField]
		private string _point_name;
		public string point_name
		{
			get { return _point_name;}
			set { _point_name = value;}
		}
		[SerializeField]
		private int _point_property;
		public int point_property
		{
			get { return _point_property;}
			set { _point_property = value;}
		}
		[SerializeField]
		private int _monster_type;
		public int monster_type
		{
			get { return _monster_type;}
			set { _monster_type = value;}
		}
		[SerializeField]
		private int _monster_count;
		public int monster_count
		{
			get { return _monster_count;}
			set { _monster_count = value;}
		}
		[SerializeField]
		private List<int> _hp_condition_rate;
		public List<int> hp_condition_rate
		{
			get { return _hp_condition_rate;}
			set { _hp_condition_rate = value;}
		}
		[SerializeField]
		private List<string> _hp_condition_dialog;
		public List<string> hp_condition_dialog
		{
			get { return _hp_condition_dialog;}
			set { _hp_condition_dialog = value;}
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
		[SerializeField]
		private int _middle_reward_type;
		public int middle_reward_type
		{
			get { return _middle_reward_type;}
			set { _middle_reward_type = value;}
		}
		[SerializeField]
		private int _middle_reward_idx;
		public int middle_reward_idx
		{
			get { return _middle_reward_idx;}
			set { _middle_reward_idx = value;}
		}
		[SerializeField]
		private int _middle_reward_value;
		public int middle_reward_value
		{
			get { return _middle_reward_value;}
			set { _middle_reward_value = value;}
		}
		[SerializeField]
		private int _cooltime;
		public int cooltime
		{
			get { return _cooltime;}
			set { _cooltime = value;}
		}
		[SerializeField]
		private List<int> _chance_rate;
		public List<int> chance_rate
		{
			get { return _chance_rate;}
			set { _chance_rate = value;}
		}
		[SerializeField]
		private List<int> _chance_hp;
		public List<int> chance_hp
		{
			get { return _chance_hp;}
			set { _chance_hp = value;}
		}
		[SerializeField]
		private List<int> _chance_touch_count;
		public List<int> chance_touch_count
		{
			get { return _chance_touch_count;}
			set { _chance_touch_count = value;}
		}
		[SerializeField]
		private List<int> _chance_touch_recovery;
		public List<int> chance_touch_recovery
		{
			get { return _chance_touch_recovery;}
			set { _chance_touch_recovery = value;}
		}
		[SerializeField]
		private int _chance_appear_count;
		public int chance_appear_count
		{
			get { return _chance_appear_count;}
			set { _chance_appear_count = value;}
		}
		[SerializeField]
		private string _Image;
		public string Image
		{
			get { return _Image;}
			set { _Image = value;}
		}
		[SerializeField]
		private string _monster_prefab;
		public string monster_prefab
		{
			get { return _monster_prefab;}
			set { _monster_prefab = value;}
		}
		[SerializeField]
		private int _monster_skin;
		public int monster_skin
		{
			get { return _monster_skin;}
			set { _monster_skin = value;}
		}
		[SerializeField]
		private string _bg_Image;
		public string bg_Image
		{
			get { return _bg_Image;}
			set { _bg_Image = value;}
		}
		[SerializeField]
		private string _monster_image;
		public string monster_image
		{
			get { return _monster_image;}
			set { _monster_image = value;}
		}

    }

    [System.Serializable]
    public class ExpeditionConquest : Table<ExpeditionConquestData, int>
    {
    }
}

