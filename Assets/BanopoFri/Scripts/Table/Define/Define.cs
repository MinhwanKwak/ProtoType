using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class DefineData
    {
        [SerializeField]
		private string _idx;
		public string idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _value;
		public int value
		{
			get { return _value;}
			set { _value = value;}
		}

    }

    [System.Serializable]
    public class Define : Table<DefineData, string>
    {
    }
}

