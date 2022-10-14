using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ShopCardpackData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
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
		private int _random_card_idx;
		public int random_card_idx
		{
			get { return _random_card_idx;}
			set { _random_card_idx = value;}
		}
		[SerializeField]
		private int _guarantee_manager_type;
		public int guarantee_manager_type
		{
			get { return _guarantee_manager_type;}
			set { _guarantee_manager_type = value;}
		}
		[SerializeField]
		private int _guarantee_grade;
		public int guarantee_grade
		{
			get { return _guarantee_grade;}
			set { _guarantee_grade = value;}
		}
		[SerializeField]
		private int _price_idx;
		public int price_idx
		{
			get { return _price_idx;}
			set { _price_idx = value;}
		}
		[SerializeField]
		private int _price_value;
		public int price_value
		{
			get { return _price_value;}
			set { _price_value = value;}
		}
		[SerializeField]
		private int _gem_price;
		public int gem_price
		{
			get { return _gem_price;}
			set { _gem_price = value;}
		}
		[SerializeField]
		private string _resource_prefeb;
		public string resource_prefeb
		{
			get { return _resource_prefeb;}
			set { _resource_prefeb = value;}
		}
		[SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}

    }

    [System.Serializable]
    public class ShopCardpack : Table<ShopCardpackData, int>
    {
    }
}

