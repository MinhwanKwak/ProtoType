using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class HeroPropertyData
    {
        [SerializeField]
		private int _property_idx;
		public int property_idx
		{
			get { return _property_idx;}
			set { _property_idx = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _dragon_idx;
		public int dragon_idx
		{
			get { return _dragon_idx;}
			set { _dragon_idx = value;}
		}
		[SerializeField]
		private int _effective_property;
		public int effective_property
		{
			get { return _effective_property;}
			set { _effective_property = value;}
		}
		[SerializeField]
		private string _icon;
		public string icon
		{
			get { return _icon;}
			set { _icon = value;}
		}
		[SerializeField]
		private string _dragonhunt_icon;
		public string dragonhunt_icon
		{
			get { return _dragonhunt_icon;}
			set { _dragonhunt_icon = value;}
		}
		[SerializeField]
		private string _dragon_icon;
		public string dragon_icon
		{
			get { return _dragon_icon;}
			set { _dragon_icon = value;}
		}

    }

    [System.Serializable]
    public class HeroProperty : Table<HeroPropertyData, int>
    {
    }
}

