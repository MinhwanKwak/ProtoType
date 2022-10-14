using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class LocalizeData
    {
        [SerializeField]
		private string _idx;
		public string idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private string _ko;
		public string ko
		{
			get { return _ko;}
			set { _ko = value;}
		}
		[SerializeField]
		private string _en;
		public string en
		{
			get { return _en;}
			set { _en = value;}
		}
		[SerializeField]
		private string _ja;
		public string ja
		{
			get { return _ja;}
			set { _ja = value;}
		}
		[SerializeField]
		private string _th;
		public string th
		{
			get { return _th;}
			set { _th = value;}
		}
		[SerializeField]
		private string _fr;
		public string fr
		{
			get { return _fr;}
			set { _fr = value;}
		}
		[SerializeField]
		private string _de;
		public string de
		{
			get { return _de;}
			set { _de = value;}
		}

    }

    [System.Serializable]
    public class Localize : Table<LocalizeData, string>
    {
    }
}

