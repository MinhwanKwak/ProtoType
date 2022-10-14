using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class PentHouseData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _type;
		public int type
		{
			get { return _type;}
			set { _type = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _category;
		public int category
		{
			get { return _category;}
			set { _category = value;}
		}
		[SerializeField]
		private int _penthouse_set_idx;
		public int penthouse_set_idx
		{
			get { return _penthouse_set_idx;}
			set { _penthouse_set_idx = value;}
		}
		[SerializeField]
		private int _grade;
		public int grade
		{
			get { return _grade;}
			set { _grade = value;}
		}
		[SerializeField]
		private int _gem_price;
		public int gem_price
		{
			get { return _gem_price;}
			set { _gem_price = value;}
		}
		[SerializeField]
		private int _max_level;
		public int max_level
		{
			get { return _max_level;}
			set { _max_level = value;}
		}
		[SerializeField]
		private List<int> _properties_type;
		public List<int> properties_type
		{
			get { return _properties_type;}
			set { _properties_type = value;}
		}
		[SerializeField]
		private int _price_type;
		public int price_type
		{
			get { return _price_type;}
			set { _price_type = value;}
		}
		[SerializeField]
		private int _random_type;
		public int random_type
		{
			get { return _random_type;}
			set { _random_type = value;}
		}
		[SerializeField]
		private int _price;
		public int price
		{
			get { return _price;}
			set { _price = value;}
		}
		[SerializeField]
		private int _point;
		public int point
		{
			get { return _point;}
			set { _point = value;}
		}
		[SerializeField]
		private string _resource_icon;
		public string resource_icon
		{
			get { return _resource_icon;}
			set { _resource_icon = value;}
		}
		[SerializeField]
		private string _resource_prefab;
		public string resource_prefab
		{
			get { return _resource_prefab;}
			set { _resource_prefab = value;}
		}

    }

    [System.Serializable]
    public class PentHouse : Table<PentHouseData, int>
    {
    }
}

