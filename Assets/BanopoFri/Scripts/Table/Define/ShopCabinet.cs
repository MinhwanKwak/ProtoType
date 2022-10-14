using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ShopCabinetData
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
		private int _popup_type;
		public int popup_type
		{
			get { return _popup_type;}
			set { _popup_type = value;}
		}
		[SerializeField]
		private int _type;
		public int type
		{
			get { return _type;}
			set { _type = value;}
		}
		[SerializeField]
		private int _package_order;
		public int package_order
		{
			get { return _package_order;}
			set { _package_order = value;}
		}
		[SerializeField]
		private int _group;
		public int group
		{
			get { return _group;}
			set { _group = value;}
		}
		[SerializeField]
		private int _group_order;
		public int group_order
		{
			get { return _group_order;}
			set { _group_order = value;}
		}
		[SerializeField]
		private int _open_first_type;
		public int open_first_type
		{
			get { return _open_first_type;}
			set { _open_first_type = value;}
		}
		[SerializeField]
		private int _open_first_idx;
		public int open_first_idx
		{
			get { return _open_first_idx;}
			set { _open_first_idx = value;}
		}
		[SerializeField]
		private int _open_first_count;
		public int open_first_count
		{
			get { return _open_first_count;}
			set { _open_first_count = value;}
		}
		[SerializeField]
		private int _open_second_type;
		public int open_second_type
		{
			get { return _open_second_type;}
			set { _open_second_type = value;}
		}
		[SerializeField]
		private int _open_second_idx;
		public int open_second_idx
		{
			get { return _open_second_idx;}
			set { _open_second_idx = value;}
		}
		[SerializeField]
		private int _open_second_count;
		public int open_second_count
		{
			get { return _open_second_count;}
			set { _open_second_count = value;}
		}
		[SerializeField]
		private int _open_routine;
		public int open_routine
		{
			get { return _open_routine;}
			set { _open_routine = value;}
		}
		[SerializeField]
		private int _limit_time;
		public int limit_time
		{
			get { return _limit_time;}
			set { _limit_time = value;}
		}
		[SerializeField]
		private int _reward_group_idx;
		public int reward_group_idx
		{
			get { return _reward_group_idx;}
			set { _reward_group_idx = value;}
		}
		[SerializeField]
		private int _shop_show;
		public int shop_show
		{
			get { return _shop_show;}
			set { _shop_show = value;}
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
		private int _open_immediate;
		public int open_immediate
		{
			get { return _open_immediate;}
			set { _open_immediate = value;}
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
		private int _price_type;
		public int price_type
		{
			get { return _price_type;}
			set { _price_type = value;}
		}
		[SerializeField]
		private int _price;
		public int price
		{
			get { return _price;}
			set { _price = value;}
		}
		[SerializeField]
		private int _display_value;
		public int display_value
		{
			get { return _display_value;}
			set { _display_value = value;}
		}
		[SerializeField]
		private string _product_id;
		public string product_id
		{
			get { return _product_id;}
			set { _product_id = value;}
		}
		[SerializeField]
		private string _resource_icon;
		public string resource_icon
		{
			get { return _resource_icon;}
			set { _resource_icon = value;}
		}
		[SerializeField]
		private string _bg_resource_icon;
		public string bg_resource_icon
		{
			get { return _bg_resource_icon;}
			set { _bg_resource_icon = value;}
		}
		[SerializeField]
		private int _use_place;
		public int use_place
		{
			get { return _use_place;}
			set { _use_place = value;}
		}
		[SerializeField]
		private float _sale_price;
		public float sale_price
		{
			get { return _sale_price;}
			set { _sale_price = value;}
		}

    }

    [System.Serializable]
    public class ShopCabinet : Table<ShopCabinetData, int>
    {
    }
}

