using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class PointItemData
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
		private int _point;
		public int point
		{
			get { return _point;}
			set { _point = value;}
		}
		[SerializeField]
		private string _icon;
		public string icon
		{
			get { return _icon;}
			set { _icon = value;}
		}

    }

    [System.Serializable]
    public class PointItem : Table<PointItemData, KeyValuePair<int,int>>
    {
    }
}

