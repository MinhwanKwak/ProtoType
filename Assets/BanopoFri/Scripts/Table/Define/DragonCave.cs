using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class DragonCaveData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private List<int> _state;
		public List<int> state
		{
			get { return _state;}
			set { _state = value;}
		}
		[SerializeField]
		private string _name_of_dragon;
		public string name_of_dragon
		{
			get { return _name_of_dragon;}
			set { _name_of_dragon = value;}
		}
		[SerializeField]
		private List<string> _name_of_dragon_state;
		public List<string> name_of_dragon_state
		{
			get { return _name_of_dragon_state;}
			set { _name_of_dragon_state = value;}
		}
		[SerializeField]
		private List<string> _icon;
		public List<string> icon
		{
			get { return _icon;}
			set { _icon = value;}
		}
		[SerializeField]
		private string _dragon_hunt_reward_icon;
		public string dragon_hunt_reward_icon
		{
			get { return _dragon_hunt_reward_icon;}
			set { _dragon_hunt_reward_icon = value;}
		}
		[SerializeField]
		private int _type;
		public int type
		{
			get { return _type;}
			set { _type = value;}
		}
		[SerializeField]
		private int _partner_hero;
		public int partner_hero
		{
			get { return _partner_hero;}
			set { _partner_hero = value;}
		}
		[SerializeField]
		private string _property_icon;
		public string property_icon
		{
			get { return _property_icon;}
			set { _property_icon = value;}
		}

    }

    [System.Serializable]
    public class DragonCave : Table<DragonCaveData, int>
    {
    }
}

