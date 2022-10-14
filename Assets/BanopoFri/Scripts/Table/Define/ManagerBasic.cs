using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ManagerBasicData
    {
        [SerializeField]
		private int _manager_idx;
		public int manager_idx
		{
			get { return _manager_idx;}
			set { _manager_idx = value;}
		}
		[SerializeField]
		private int _facility_idx;
		public int facility_idx
		{
			get { return _facility_idx;}
			set { _facility_idx = value;}
		}
		[SerializeField]
		private int _buff_idx;
		public int buff_idx
		{
			get { return _buff_idx;}
			set { _buff_idx = value;}
		}
		[SerializeField]
		private int _skill_buff_idx;
		public int skill_buff_idx
		{
			get { return _skill_buff_idx;}
			set { _skill_buff_idx = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _max_level;
		public int max_level
		{
			get { return _max_level;}
			set { _max_level = value;}
		}
		[SerializeField]
		private int _manage_type;
		public int manage_type
		{
			get { return _manage_type;}
			set { _manage_type = value;}
		}
		[SerializeField]
		private int _manage_grade;
		public int manage_grade
		{
			get { return _manage_grade;}
			set { _manage_grade = value;}
		}
		[SerializeField]
		private string _manager_icon;
		public string manager_icon
		{
			get { return _manager_icon;}
			set { _manager_icon = value;}
		}
		[SerializeField]
		private int _gem_price;
		public int gem_price
		{
			get { return _gem_price;}
			set { _gem_price = value;}
		}
		[SerializeField]
		private string _ingame_prefab_path;
		public string ingame_prefab_path
		{
			get { return _ingame_prefab_path;}
			set { _ingame_prefab_path = value;}
		}
		[SerializeField]
		private int _point;
		public int point
		{
			get { return _point;}
			set { _point = value;}
		}
		[SerializeField]
		private int _use_place;
		public int use_place
		{
			get { return _use_place;}
			set { _use_place = value;}
		}

    }

    [System.Serializable]
    public class ManagerBasic : Table<ManagerBasicData, int>
    {
    }
}

