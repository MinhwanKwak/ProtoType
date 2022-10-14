using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ShopCurrencyData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _open_stage;
		public int open_stage
		{
			get { return _open_stage;}
			set { _open_stage = value;}
		}
		[SerializeField]
		private int _close_stage;
		public int close_stage
		{
			get { return _close_stage;}
			set { _close_stage = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _currency_idx;
		public int currency_idx
		{
			get { return _currency_idx;}
			set { _currency_idx = value;}
		}
		[SerializeField]
		private int _currency_value;
		public int currency_value
		{
			get { return _currency_value;}
			set { _currency_value = value;}
		}
		[SerializeField]
		private int _bonus_value;
		public int bonus_value
		{
			get { return _bonus_value;}
			set { _bonus_value = value;}
		}
		[SerializeField]
		private int _bonus_count;
		public int bonus_count
		{
			get { return _bonus_count;}
			set { _bonus_count = value;}
		}
		[SerializeField]
		private string _bonus_tag_name;
		public string bonus_tag_name
		{
			get { return _bonus_tag_name;}
			set { _bonus_tag_name = value;}
		}
		[SerializeField]
		private int _price;
		public int price
		{
			get { return _price;}
			set { _price = value;}
		}
		[SerializeField]
		private string _resource_icon;
		public string resource_icon
		{
			get { return _resource_icon;}
			set { _resource_icon = value;}
		}
		[SerializeField]
		private string _product_id;
		public string product_id
		{
			get { return _product_id;}
			set { _product_id = value;}
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
    public class ShopCurrency : Table<ShopCurrencyData, int>
    {
    }
}

