using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class BuffData
    {
        [SerializeField]
		private int _buff_idx;
		public int buff_idx
		{
			get { return _buff_idx;}
			set { _buff_idx = value;}
		}
		[SerializeField]
		private string _buff_name;
		public string buff_name
		{
			get { return _buff_name;}
			set { _buff_name = value;}
		}
		[SerializeField]
		private string _buff_icon;
		public string buff_icon
		{
			get { return _buff_icon;}
			set { _buff_icon = value;}
		}
		[SerializeField]
		private int _buff_type;
		public int buff_type
		{
			get { return _buff_type;}
			set { _buff_type = value;}
		}
		[SerializeField]
		private int _target_type;
		public int target_type
		{
			get { return _target_type;}
			set { _target_type = value;}
		}
		[SerializeField]
		private int _target_idx;
		public int target_idx
		{
			get { return _target_idx;}
			set { _target_idx = value;}
		}
		[SerializeField]
		private int _buff_value;
		public int buff_value
		{
			get { return _buff_value;}
			set { _buff_value = value;}
		}

    }

    [System.Serializable]
    public class Buff : Table<BuffData, int>
    {
    }
}

