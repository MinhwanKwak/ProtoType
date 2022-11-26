using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ItemShopData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _category;
		public int category
		{
			get { return _category;}
			set { _category = value;}
		}
		[SerializeField]
		private int _group;
		public int group
		{
			get { return _group;}
			set { _group = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private int _item_idx;
		public int item_idx
		{
			get { return _item_idx;}
			set { _item_idx = value;}
		}
		[SerializeField]
		private List<int> _item_stock_value;
		public List<int> item_stock_value
		{
			get { return _item_stock_value;}
			set { _item_stock_value = value;}
		}
		[SerializeField]
		private int _cost_idx;
		public int cost_idx
		{
			get { return _cost_idx;}
			set { _cost_idx = value;}
		}
		[SerializeField]
		private int _cost_value;
		public int cost_value
		{
			get { return _cost_value;}
			set { _cost_value = value;}
		}

    }

    [System.Serializable]
    public class ItemShop : Table<ItemShopData, KeyValuePair<int,int>>
    {
    }
}

