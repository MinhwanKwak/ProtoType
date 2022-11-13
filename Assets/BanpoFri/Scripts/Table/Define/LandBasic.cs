using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class LandBasicData
    {
        [SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}
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
		private byte[] _open_cost;
		public System.Numerics.BigInteger open_cost
		{
			get { return new System.Numerics.BigInteger(_open_cost);}
			set { _open_cost = value.ToByteArray();}
		}
		[SerializeField]
		private int _default_point;
		public int default_point
		{
			get { return _default_point;}
			set { _default_point = value;}
		}
		[SerializeField]
		private List<string> _icon;
		public List<string> icon
		{
			get { return _icon;}
			set { _icon = value;}
		}
		[SerializeField]
		private List<string> _prefab;
		public List<string> prefab
		{
			get { return _prefab;}
			set { _prefab = value;}
		}

    }

    [System.Serializable]
    public class LandBasic : Table<LandBasicData, KeyValuePair<int,int>>
    {
    }
}

