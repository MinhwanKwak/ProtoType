using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ShopVipData
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
		private int _price;
		public int price
		{
			get { return _price;}
			set { _price = value;}
		}
		[SerializeField]
		private string _product_id;
		public string product_id
		{
			get { return _product_id;}
			set { _product_id = value;}
		}

    }

    [System.Serializable]
    public class ShopVip : Table<ShopVipData, int>
    {
    }
}

