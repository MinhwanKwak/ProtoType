using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class InHouseBannerData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private string _onelink_url;
		public string onelink_url
		{
			get { return _onelink_url;}
			set { _onelink_url = value;}
		}
		[SerializeField]
		private string _banner_resource;
		public string banner_resource
		{
			get { return _banner_resource;}
			set { _banner_resource = value;}
		}
		[SerializeField]
		private string _banner_resource_small;
		public string banner_resource_small
		{
			get { return _banner_resource_small;}
			set { _banner_resource_small = value;}
		}
		[SerializeField]
		private int _reward_gem_count;
		public int reward_gem_count
		{
			get { return _reward_gem_count;}
			set { _reward_gem_count = value;}
		}
		[SerializeField]
		private int _display_aos;
		public int display_aos
		{
			get { return _display_aos;}
			set { _display_aos = value;}
		}
		[SerializeField]
		private int _display_ios;
		public int display_ios
		{
			get { return _display_ios;}
			set { _display_ios = value;}
		}

    }

    [System.Serializable]
    public class InHouseBanner : Table<InHouseBannerData, int>
    {
    }
}

