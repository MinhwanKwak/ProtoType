using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class CurrencyData
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
		private string _img;
		public string img
		{
			get { return _img;}
			set { _img = value;}
		}

    }

    [System.Serializable]
    public class Currency : Table<CurrencyData, int>
    {
    }
}

